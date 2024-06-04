namespace Names;

internal static class HeatmapTask
{
    public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
    {
        var dayNumber = new string[30];
        var monthNumber = new string[12];
        for (int i = 0; i < 30; i++)
        {
            if (i < 12)
                monthNumber[i] = (1 + i).ToString();
            dayNumber[i] = (2 + i).ToString();
        }
        var dayOfMounth = new double[30, 12];
        foreach (var item in names)
        {
            if (item.BirthDate.Day != 1)
                dayOfMounth[item.BirthDate.Day - 2, item.BirthDate.Month - 1]++;
        }
        return new HeatmapData(
            "Пример карты интенсивностей",
            dayOfMounth,
            dayNumber,
            monthNumber);
    }
}