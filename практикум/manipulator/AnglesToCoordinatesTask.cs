using System;
using Avalonia;
using NUnit.Framework;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class AnglesToCoordinatesTask
{
    /// <summary>
    /// По значению углов суставов возвращает массив координат суставов
    /// в порядке new []{elbow, wrist, palmEnd}
    /// </summary>
    public static Point[] GetJointPositions(double shoulder, double elbow, double wrist)
    {
        var elbowPos = new Point(Manipulator.UpperArm * (float)Math.Cos(shoulder),
            Manipulator.UpperArm * (float)Math.Sin(shoulder));
        var angle = shoulder + elbow - Math.PI;
        var wristPos = new Point(elbowPos.X + Manipulator.Forearm * (float)Math.Cos(angle),
            elbowPos.Y + Manipulator.Forearm * (float)Math.Sin(angle));
        angle += wrist - Math.PI;
        var palmEndPos = new Point(wristPos.X + Manipulator.Palm * (float)Math.Cos(angle),
            wristPos.Y + Manipulator.Palm * (float)Math.Sin(angle));
        return new[]
        {
            elbowPos,
            wristPos,
            palmEndPos
        };
    }
}

[TestFixture]
public class AnglesToCoordinatesTask_Tests
{
    // Доработайте эти тесты!
    // С помощью строчки TestCase можно добавлять новые тестовые данные.
    // Аргументы TestCase превратятся в аргументы метода.
    [TestCase(0, Math.PI, Math.PI, Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm, 0)]
    [TestCase(Math.PI / 2, Math.PI / 2, Math.PI / 2, Manipulator.Forearm, Manipulator.UpperArm - Manipulator.Palm)]
    [TestCase(Math.PI, 3 * Math.PI / 2, Math.PI / 2, -Manipulator.UpperArm - Manipulator.Palm, -Manipulator.Forearm)]
    [TestCase(Math.PI / 2, 3 * Math.PI / 2, 3 * Math.PI / 2, -Manipulator.Forearm, Manipulator.UpperArm - Manipulator.Palm)]
    [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
    [TestCase(3 * Math.PI / 2, 3 * Math.PI / 2, 3 * Math.PI / 2, Manipulator.Forearm, -Manipulator.UpperArm + Manipulator.Palm)]
    public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
    {
        var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
        Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
    }
}