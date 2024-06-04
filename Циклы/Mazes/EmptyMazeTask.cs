namespace Mazes;

public static class EmptyMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        for (int i = 0; i < width + height - 2; i++)
        {
            if (robot.Y != height - 2 && !robot.Finished) robot.MoveTo(Direction.Down);
            if (robot.X != width - 2 && !robot.Finished) robot.MoveTo(Direction.Right);
        }
    }
}