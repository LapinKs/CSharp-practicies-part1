namespace Passwords;

public class CaseAlternatorTask
{
    public static List<string> AlternateCharCases(string lowercaseWord)
    {
        var result = new List<string>();
        AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
        return result;
    }

    static void AlternateCharCases(char[] word, int startIndex, List<string> result)
    {
        if (startIndex == word.Length)
        {
            if (result.Contains(new string(word)) == false)
            {
                result.Add(new string(word));
            }
            return;
        }
        if (char.IsLetter(word[startIndex]))
        {
            word[startIndex] = char.ToLower(word[startIndex]);
            AlternateCharCases(word, 1 + startIndex, result);
            word[startIndex] = char.ToUpper(word[startIndex]);
            AlternateCharCases(word, 1 + startIndex, result);
        }
        else
        {
            AlternateCharCases(word, 1 + startIndex, result);
        }
    }
}