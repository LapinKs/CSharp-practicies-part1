namespace Pluralize;

public static class PluralizeTask
{
    public static string PluralizeRubles(int count)
    {
        string output = "";
        if ((count % 10 == 0) || (count % 10 >= 5) && (count % 10 <= 9) || ((count % 100 > 10) && ((count % 100 < 20)))) output = "рублей";
        else if (count % 10 == 1) output = "рубль";
        else if ((count % 10 >= 2) && (count % 10 <= 4)) output = "рубля";
        return output;
    }
}