namespace AoC2024.Days
{
    public class Day1CommandHandler
    {
        public static double Execute(string destination, string? part)
        {
            var lines = File.ReadAllLines(destination);

            var firstList = new List<int>();
            var secondList = new List<int>();

            double final = 0;

            foreach (var line in lines)
            {
                var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var first = int.Parse(numbers[0]);
                var second = int.Parse(numbers[1]);

                firstList.Add(first);
                secondList.Add(second);
            }

            switch (part) {
                case "part1":
                    final = CalculatePart1(firstList, secondList);
                    break;
                case "part2":
                    final = CalculatePart2(firstList, secondList);
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

        private static double CalculatePart2(List<int> firstList, List<int> secondList)
        {
            var distance = 0;

            firstList.Sort();
            secondList.Sort();

            for (int i = 0; i < firstList.Count; i++)
            {
                distance += Math.Abs(firstList[i] - secondList[i]);
            }

            return distance;
        }

        private static double CalculatePart1(List<int> firstList, List<int> secondList)
        {
            var similarity = 0;

            Dictionary<int, int> numberCounts = secondList
                .GroupBy(n => n)
                .ToDictionary(group => group.Key, group => group.Count());

            foreach (var num in firstList)
            {
                if (numberCounts.ContainsKey(num))
                {
                    similarity += numberCounts[num] * num;
                }
            }

            return similarity;
        }
    }
}
