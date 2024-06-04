using System;
using NUnit.Framework;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class ManipulatorTask
{
    /// <summary>
    /// Возвращает массив углов (shoulder, elbow, wrist),
    /// необходимых для приведения эффектора манипулятора в точку x и y 
    /// с углом между последним суставом и горизонталью, равному alpha (в радианах)
    /// См. чертеж manipulator.png!
    /// </summary>
    public static double[] MoveManipulatorTo(double x, double y, double alpha)
    {
        var wristX = x + Math.Cos(Math.PI - alpha) * Manipulator.Palm;
        var wristY = y + Math.Sin(Math.PI - alpha) * Manipulator.Palm;
        var side = Math.Sqrt(wristX * wristX + wristY * wristY);
        double elbow = TriangleTask.GetABAngle(Manipulator.Forearm, Manipulator.UpperArm, side);
        double shoulderOne = Math.Atan2(wristY, wristX);
        double shoulderTwo = TriangleTask.GetABAngle(side, Manipulator.UpperArm, Manipulator.Forearm);
        var shoulder = shoulderOne + shoulderTwo;
        double wrist = 2 * Math.PI - elbow - alpha - shoulder;
        if (shoulder == double.NaN || elbow == double.NaN || wrist == double.NaN)
            return new[] { double.NaN, double.NaN, double.NaN };
        return new[] { shoulder, elbow, wrist };
    }
}

[TestFixture]
public class ManipulatorTask_Tests
{
    [Test]
    public void TestMoveManipulatorTo()
    {
        Random rnd = new Random();
        double x = rnd.NextDouble();
        double y = rnd.NextDouble();
        double angle = rnd.NextDouble();
        var rndAngles = ManipulatorTask.MoveManipulatorTo(x, y, angle);
        var joints = AnglesToCoordinatesTask.GetJointPositions(rndAngles[0], rndAngles[1], rndAngles[2]);
        Assert.AreEqual(joints[2].X, x, 1e-3);
        Assert.AreEqual(joints[2].Y, y, 1e-3);

    }
}