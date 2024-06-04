using System;
using System.Drawing;

namespace RoutePlanning;

public static class PathFinderTask
{
    public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
    {
        double best = double.MaxValue;
        var bestOrder = new int[checkpoints.Length];
        MakeTrivialPermutation(1, 0, new int[checkpoints.Length], bestOrder, checkpoints, ref best);
        return bestOrder;
    }

    private static void MakeTrivialPermutation(int pos, double current,
        int[] permutation, int[] bestPermutation,
        Point[] checkpoints, ref double best)
    {
        if (best < current)
            return;
        if (pos == permutation.Length)
        {
            best = current;
            permutation.CopyTo(bestPermutation, 0);
            return;
        }
        for (int i = 0; i < permutation.Length; i++)
        {
            if (Array.IndexOf(permutation, i, 0, pos) != -1)
                continue;
            permutation[pos] = i;
            var plus = PointExtensions.DistanceTo(checkpoints[permutation[pos]],
                checkpoints[permutation[pos - 1]]);
            MakeTrivialPermutation(pos + 1, current + plus, permutation, bestPermutation, checkpoints, ref best);
        }
    }
}