namespace Names;

internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        var birthDay = new double[31];
        var day = new string[31];
        for (int i = 0; i < 31; i++)
        {
            day[i] = (1 + i).ToString();
        }
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].BirthDate.Day != 1 && names[i].Name == name)
                birthDay[names[i].BirthDate.Day - 1]++;
        }
        return new HistogramData(
            $"Рождаемость людей с именем '{name}'",
            day,
            birthDay);
    }
}