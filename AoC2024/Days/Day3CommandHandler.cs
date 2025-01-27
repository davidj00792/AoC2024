
using System.Linq.Expressions;
using System.Text.RegularExpressions;

internal class Day3CommandHandler
{
    internal static double Execute(string destination, string? part)
    {
        var content =  File.ReadAllText(destination);
        double final = 0;

        switch (part)
        {
            case "1":
                final = CalculatePart1(content);
                break;
            case "2":
                final = CalculatePart2(content);
                break;
            case null:
                Console.WriteLine("enter some part");
                break;
            default:
                Console.WriteLine("invalid part");
                break;
        }


        return final;
    }

    private static double CalculatePart2(string content)
    {
        string pattern = @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)";
        string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var sequences = FindMulPatterns(content, pattern);

        var result = 0;
        var calculate = true;

        foreach (var sequence in sequences)
        {
            if (Regex.IsMatch(sequence, mulPattern) && calculate)
            {
                Match match = Regex.Match(sequence, mulPattern);
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                result += x * y;
            }
            else if (sequence == "do()")
            {
                calculate = true;
            }
            else if (sequence == "don't()")
            {
                calculate = false;
            }
        }

        return result;
    }

    private static double CalculatePart1(string content)
    {
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var sequences = FindMulPatterns(content, pattern);

        var result = 0;

        foreach (var sequence in sequences) 
        {
            Match match = Regex.Match(sequence, pattern);

            int x = int.Parse(match.Groups[1].Value);
            int y = int.Parse(match.Groups[2].Value);

            result += x * y;
        }

        return result;
    }

    static List<string> FindMulPatterns(string text, string pattern)
    {
        MatchCollection matches = Regex.Matches(text, pattern);
        List<string> result = new List<string>();
        foreach (Match match in matches)
        {
            result.Add(match.Value);
        }

        return result;
    }
}