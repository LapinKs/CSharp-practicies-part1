using NUnit.Framework;
using System.Collections.Generic;
namespace Recognizer;

public static class ThresholdFilterTask
{
    public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
    {
        int width = original.GetLength(0);
        int height = original.GetLength(1);
        int PixtoWhite = (int)(width * height * whitePixelsFraction);
        var result = new double[width, height];
        var list = new List<double>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
                list.Add(original[i, j]);
        }
        list.Sort();
        list.RemoveRange(0, list.Count - PixtoWhite);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                result[x, y] = list.Contains(original[x, y]) ? 1.0 : 0.0;
            }
        }
        return result;
    }
}
