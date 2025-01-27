
internal class Day5CommandHandler
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
                final = CalculatePart2(lines); //To do, incorrect
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

    private static double CalculatePart2(string[] lines)
    {
        var listXY = new List<(int X, int Y)>();
        var listXYZ = new List<string>();
        var middlePageSum = 0;

        // Parsing flag
        bool isSecondSection = false;

        var orderingRules = new Dictionary<int, (HashSet<int> Before, HashSet<int> After)>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                // Switch to the second section after the empty line
                isSecondSection = true;
                continue;
            }

            if (!isSecondSection)
            {
                // Parse rows in X|Y format
                var parts = line.Split('|');

                var num1 = int.Parse(parts[0]);
                var num2 = int.Parse(parts[1]);

                AddNumberToRules(orderingRules, num1, num2);
            }
            else
            {
                listXYZ.Add(line);
            }
        }

        foreach (var line in listXYZ)
        {
            List<int> numbers = line
                .Split(',')
                .Select(int.Parse)
                .ToList();

            bool isCorrect = CheckOrder(numbers, orderingRules);

            if (!isCorrect)
            {
                List<int> sortedNumbers = Order(numbers, orderingRules);

                var middlePage = float.Round(sortedNumbers.Count / 2, 0, MidpointRounding.AwayFromZero);
                var middlePageValue = sortedNumbers[(int)middlePage];
                middlePageSum += middlePageValue;
            }
        }



        return middlePageSum;
    }

    private static List<int> Order(List<int> numbers, Dictionary<int, (HashSet<int> Before, HashSet<int> After)> orderingRules)
    {
        var orderedList = new List<int>();
        var numbersCount = numbers.Count;
        var found = false;

        for (int i = 0; i < numbers.Count; i++)
        {
            var numberToCheck = numbers[i];

            if (!orderingRules.ContainsKey(numberToCheck))
            {
                orderedList.Add(numberToCheck);
                continue;
            }

            for (int j = i + 1; j < numbers.Count; j++)
            {
                if (orderingRules[numberToCheck].Before.Contains(numbers[j]))
                {
                    orderedList.Add(numbers[j]);
                    numbers.RemoveAt(j);
                    i--;
                    found = true;
                    break;
                }
                found = false;
            }

            if (!found)
            {
                orderedList.Add(numberToCheck);
                found = false;
            }
        }

        if (orderedList.Count != numbersCount)
        {
            orderedList.Add(numbers.Last());
        }

        return orderedList;
    }

    // 1 2 13(2) 5

    private static double CalculatePart1(string[] lines)
    {
        var listXY = new List<(int X, int Y)>();
        var listXYZ = new List<string>();
        var middlePageSum = 0;


        // Parsing flag
        bool isSecondSection = false;

        var orderingRules = new Dictionary<int, (HashSet<int> Before, HashSet<int> After)>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                // Switch to the second section after the empty line
                isSecondSection = true;
                continue;
            }

            if (!isSecondSection)
            {
                // Parse rows in X|Y format
                var parts = line.Split('|');

                var num1 = int.Parse(parts[0]);
                var num2 = int.Parse(parts[1]);

                AddNumberToRules(orderingRules, num1, num2);
            }
            else
            {
                listXYZ.Add(line);
            }
        }

        foreach (var line in listXYZ)
        {
            List<int> numbers = line
                .Split(',') 
                .Select(int.Parse) 
                .ToList();

            bool isCorrect = CheckOrder(numbers, orderingRules);

            if (isCorrect)
            {
                var middlePage = float.Round(numbers.Count/2, 0, MidpointRounding.AwayFromZero);
                var middlePageValue = numbers[(int)middlePage];
                middlePageSum += middlePageValue;
            }
        }



        return middlePageSum;
    }

    private static bool CheckOrder(List<int> numbers, Dictionary<int, (HashSet<int> Before, HashSet<int> After)> orderingRules)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            var numberToCheck = numbers[i];

            if(!orderingRules.ContainsKey(numberToCheck))
            {
                continue;
            }

            for (int j = i+1; j < numbers.Count; j++)
            {
                if (orderingRules[numberToCheck].Before.Contains(numbers[j]))
                {
                    return false;
                }
            }

        }

        return true;
    }

    private static void AddNumberToRules(Dictionary<int, (HashSet<int> Before, HashSet<int> After)> orderingRules, int num1, int num2)
    {
        if (orderingRules.ContainsKey(num1))
        {
            orderingRules[num1].After.Add(num2);
        }
        else
        {
            orderingRules.Add(num1, (new HashSet<int>(), new HashSet<int> { num2 }));
        }

        if (orderingRules.ContainsKey(num2))
        {
            orderingRules[num2].Before.Add(num1);
        }
        else
        {
            orderingRules.Add(num2, (new HashSet<int> { num1 }, new HashSet<int> ()));
        }
    }
}