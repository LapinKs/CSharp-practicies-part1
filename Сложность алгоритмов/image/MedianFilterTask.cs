using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Recognizer;

internal static class MedianFilterTask
{
    /* 
	 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
	 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
	 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
	 * https://en.wikipedia.org/wiki/Median_filter
	 * 
	 * Используйте окно размером 3х3 для не граничных пикселей,
	 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
	 */
    public static double[,] MedianFilter(double[,] original)
    {
        var result = new double[original.GetLength(0), original.GetLength(1)];
        for (int x = 0; x < original.GetLength(0); x++)
        {
            for (int y = 0; y < original.GetLength(1); y++)
            {
                var semiRes = new List<double>();
                var a = new int[] { -1, 0, 1 };
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int dx = x + a[i];
                        int dy = y + a[j];
                        if (dx >= 0 && dy >= 0 && dx < original.GetLength(0) && dy < original.GetLength(1))
                            semiRes.Add(original[dx, dy]);
                    }
                }

                semiRes.Sort();
                if (semiRes.Count % 2 == 1)
                    result[x, y] = semiRes[semiRes.Count / 2];

                else
                    result[x, y] = (semiRes[semiRes.Count / 2] + semiRes[(semiRes.Count / 2 - 1)]) / 2;
            }
        }
        return result;
    }
}

