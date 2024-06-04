using System;

namespace Billiards
{
    public static class BilliardsTask
    {
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            return wallInclinationRadians + (wallInclinationRadians - directionRadians);
        }
    }
}