namespace Mazes;

public static class DiagonalMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        while (!robot.Finished)
        {
            if (width < height)
                MoveRobot(robot, Direction.Down, Direction.Right, (height - 2) / (width - 2));
            if (width > height)
                MoveRobot(robot, Direction.Right, Direction.Down, (width - 2) / (height - 2));
        }
    }

    public static void MoveRobot(Robot robot, Direction direction1, Direction direction2, int partMove)
    {
        if (!robot.Finished)
        {
            for (int i = 0; i < partMove; i++)
                robot.MoveTo(direction1);
        }
        if (!robot.Finished)
            robot.MoveTo(direction2);
    }
}