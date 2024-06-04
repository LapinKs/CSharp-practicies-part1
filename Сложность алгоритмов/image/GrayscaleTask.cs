namespace Recognizer;

public static class GrayscaleTask
{
    public static double[,] ToGrayscale(Pixel[,] original)
    {
        int height = original.GetLength(0);
        int width = original.GetLength(1);
        var result = new double[height, width];
        for (int currX = 0; currX < width; currX++)
        {
            for (int currY = 0; currY < height; currY++)
                result[currY, currX] = (0.114 * original[currY, currX].B + 0.587 * original[currY, currX].G + 0.299 * original[currY, currX].R) / 255;
        }
        return result;
    }
}
