using System;

namespace Fractals;

internal static class DragonFractalTask
{
    public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
    {
        var random = new Random(seed);
        double x = 1.0;
        double y = 0.0;
        for (int i = 0; i < iterationsCount; i++)
        {
            var next = random.Next(2);
            var ang = Math.PI * (1 + next * 2) / 4;
            var x1 = (x * Math.Cos(ang) - y * Math.Sin(ang)) / Math.Sqrt(2) + next;
            var y1 = (x * Math.Sin(ang) + y * Math.Cos(ang)) / Math.Sqrt(2);
            x = x1;
            y = y1;
            pixels.SetPixel(x, y);
        }
    }
}