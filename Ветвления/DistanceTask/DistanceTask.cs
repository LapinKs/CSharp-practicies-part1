using SkiaSharp;
using System;

namespace DistanceTask;

public static class DistanceTask
{
    // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
    public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
    {
        if ((ax == x && ay == y) || (bx == x && by == y))
            return 0;
        if ((ax == bx) && (ay == by))
            return Math.Sqrt((ax - x) * (ax - x) + (ay - y) * (ay - y));
        double one = Math.Sqrt((bx - x) * (bx - x) + (by - y) * (by - y));
        double two = Math.Sqrt((ay - y) * (ay - y) + (ax - x) * (ax - x));
        double tree = Math.Sqrt((ax - bx) * (ax - bx) + (ay - by) * (ay - by));
        if (one * one > two * two + tree * tree || two * two > one * one + tree * tree)
            return Math.Min(one, two);
        double p = (one + two + tree) / 2.0;
        double s = Math.Sqrt(Math.Abs(p * (p - one) * (p - two) * (p - tree)));
        double height = 2.0 * s / tree;
        return height;
    }
}