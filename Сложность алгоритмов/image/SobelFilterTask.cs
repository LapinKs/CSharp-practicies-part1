using System;

namespace Recognizer;
internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var width = g.GetLength(0);
        var height = g.GetLength(1);
        var result = new double[width, height];
        var side = sx.GetLength(0);
        var sy = new double[side, side];
        var matrixShift = side / 2;
        for (var i = 0; i < sx.GetLength(0); i++)
        {
            for (var j = 0; j < sx.GetLength(1); j++)
                sy[i, j] = sx[j, i];
        }
        for (int x = matrixShift; x < width - matrixShift; x++)
        {
            for (int y = matrixShift; y < height - matrixShift; y++)
            {
                double gx = SemiConvultionSquared(g, sx, x, y, matrixShift);
                double gy = SemiConvultionSquared(g, sy, x, y, matrixShift);
                result[x, y] = Math.Sqrt(gx + gy);
            }
        }
        return result;
    }
    public static double SemiConvultionSquared(double[,] pix, double[,] m, int x, int y, int mShift)
    {
        double res = 0;
        for (int i = 0; i < m.GetLength(0); i++)
        {
            for (int j = 0; j < m.GetLength(0); j++)
                res += pix[x - mShift + i, y - mShift + j] * m[i, j];
        }
        return res * res;
    }
}