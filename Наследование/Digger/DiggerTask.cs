using System;
using System.Diagnostics.Metrics;
using Avalonia.Controls;
using Avalonia.Input;
using Digger.Architecture;

namespace Digger;
public class Terrain : ICreature
{
    public string GetImageFileName() => "Terrain.png";
    public int GetDrawingPriority() => 0;
    public CreatureCommand Act(int x, int y) => new CreatureCommand { DeltaX = 0, DeltaY = 0 };
    public bool DeadInConflict(ICreature currentObject) => true;
}
public class Player : ICreature
{
    public static int posX = 0;
    public static int posY = 0;

    private bool CanMove(int x, int y) => Game.Map[x, y] == null || Game.Map[x, y] is not Digger.Sack;
    public int GetDrawingPriority() => 1;
    public string GetImageFileName() => "Digger.png";
    public bool DeadInConflict(ICreature currentObject)
    {
        if (currentObject.GetImageFileName() == "Gold.png")
            Game.Scores += 10;

        return currentObject is Digger.Sack || currentObject is Digger.Monster;
    }
    public CreatureCommand Act(int x, int y)
    {
        posX = x;
        posY = y;
        var outPut = new CreatureCommand();
        if (Game.KeyPressed == Key.Down && y < Game.MapHeight - 1 && CanMove(x, y + 1))
            return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
        if (Game.KeyPressed == Key.Up && y >= 1 && CanMove(x, y - 1))
            return new CreatureCommand { DeltaX = 0, DeltaY = -1 };
        if (Game.KeyPressed == Key.Right && x < Game.MapWidth - 1 && CanMove(x + 1, y))
            return new CreatureCommand { DeltaX = 1, DeltaY = 0 };
        if (Game.KeyPressed == Key.Left && x >= 1 && CanMove(x - 1, y))
            return new CreatureCommand { DeltaX = -1, DeltaY = 0 };
        return new CreatureCommand { DeltaX = 0, DeltaY = 0 };

    }
}

public class Sack : ICreature
{
    private int _fall = 0;
    public string GetImageFileName() => "Sack.png";
    public int GetDrawingPriority() => 2;
    public CreatureCommand Act(int x, int y)
    {
        if (y < Game.MapHeight - 1)
        {
            if (Game.Map[x, y + 1] == null || _fall > 0 && (Game.Map[x, y + 1] is Digger.Player || Game.Map[x, y + 1] is Digger.Monster))
            {
                _fall++;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
            }
        }
        if (_fall > 1)
        {
            _fall = 0;
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
        }
        _fall = 0;
        return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
    }
    public bool DeadInConflict(ICreature currentObject) => false;

}
public class Gold : ICreature
{
    public string GetImageFileName() => "Gold.png";
    public int GetDrawingPriority() => 3;
    public CreatureCommand Act(int x, int y) => new CreatureCommand { DeltaX = 0, DeltaY = 0 };
    public bool DeadInConflict(ICreature currentObject) => true;
}
public class Monster : ICreature
{
    public string GetImageFileName() => "Monster.png";
    public int GetDrawingPriority() => -1;
    public CreatureCommand Act(int x, int y)
    {
        int currX = 0;
        int currY = 0;
        if (FindThePlayer())
        {
            if (Player.posX == x && Player.posY != y)
            {
                currY = Player.posY < y ? -1 : 1;
            }
            else if (Player.posX != x && Player.posY == y)
            {
                currX = Player.posX < x ? -1 : 1;
            }
            else if (Player.posY != x && Player.posY != y)
            {
                currX = Player.posX < x ? -1 : 1;
            }

        }
        else
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        if (x + currX < 0 || x + currX > Game.MapWidth || y + currY < 0 || y + currY > Game.MapWidth)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }
        var curr = Game.Map[x + currX, y + currY];
        if (curr != null && (curr is Terrain || curr is Sack || curr is Monster))
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        return new CreatureCommand() { DeltaX = currX, DeltaY = currY };
    }
    public bool DeadInConflict(ICreature currentObject) => currentObject is Sack || currentObject is Monster;
    public bool FindThePlayer()
    {
        for (int i = 0; i < Game.MapWidth; i++)
            for (int j = 0; j < Game.MapHeight; j++)
                if (Game.Map[i, j] is Player)
                {
                    Player.posX = i;
                    Player.posY = j;
                    return true;
                }
        return false;
    }
}