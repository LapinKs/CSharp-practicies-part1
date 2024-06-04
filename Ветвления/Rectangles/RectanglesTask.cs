using System;

namespace Rectangles;

public static class RectanglesTask
{
    public static bool AreIntersected(Rectangle r1, Rectangle r2)
    {
        return !(r1.Left > r2.Right || r1.Right < r2.Left || r1.Bottom < r2.Top || r1.Top > r2.Bottom);
    }

    public static int IntersectionSquare(Rectangle r1, Rectangle r2)
    {
        if (AreIntersected(r1, r2))
        {
            if (r1.Top == r2.Bottom || r1.Bottom == r2.Top || r1.Left == r2.Right || r1.Right == r2.Left)
                return 0;
            int width = Math.Min(r1.Right, r2.Right) - Math.Max(r1.Left, r2.Left);
            int height = -Math.Max(r1.Top, r2.Top) + Math.Min(r1.Bottom, r2.Bottom);
            return height * width;
        }
        return 0;
    }

    public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
    {
        if (r1.Right <= r2.Right && r1.Left >= r2.Left && r1.Top >= r2.Top && r1.Bottom <= r2.Bottom)
            return 0;
        if (r1.Left <= r2.Left && r1.Right >= r2.Right && r1.Top <= r2.Top && r1.Bottom >= r2.Bottom)
            return 1;
        else
            return -1;
    }
}