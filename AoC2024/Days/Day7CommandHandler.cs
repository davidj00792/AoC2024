

internal class Day7CommandHandler
{
    internal static double Execute(string destination, string? part)
    {
        var lines = File.ReadAllLines(destination);
        double final = 0;

        switch (part)
        {
            case "1":
                final = CalculatePart1(lines);
                break;
            case "2":
                //final = CalculatePart2(lines); //To do, incorrect
                break;
            case null:
                Console.WriteLine("enter some part");
                break;
            default:
                Console.WriteLine("invalid part");
                break;
        }
        //Try to do it with linked list, better for moving items.

        return final;
    }

    private static double CalculatePart1(string[] lines)
    {
        var result = 0;

        foreach (var line in lines) {
            var numbers = line.Split(':');
        }

        throw new NotImplementedException();
    }

    static bool CanFormTarget(int target, List<int> numbers)
    {
        return CanFormTargetHelper(target, numbers, numbers[0], 1);
    }

    static bool CanFormTargetHelper(int target, List<int> numbers, int currentResult, int index)
    {
        if (index == numbers.Count)
        {
            return currentResult == target;
        }

        int nextNumber = numbers[index];

        // Try addition
        if (CanFormTargetHelper(target, numbers, currentResult + nextNumber, index + 1))
        {
            return true;
        }

        // Try multiplication
        if (CanFormTargetHelper(target, numbers, currentResult * nextNumber, index + 1))
        {
            return true;
        }

        return false; // If neither operation works, return false
    }
}