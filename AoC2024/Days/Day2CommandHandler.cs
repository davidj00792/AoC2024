
namespace AoC2024.Days
{
    public class Day2CommandHandler
    {
        public static double Execute(string destination, string? part)
        {
            var lines = File.ReadAllLines(destination);
            double final = 0;

            switch (part)
            {
                case "1":
                    final = CalculatePart1(lines);
                    break;
                case "2":
                    final = CalculatePart2(lines);
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

        private static double CalculatePart2(string[] lines)
        {
            var safeReports = 0;


            foreach (var line in lines)
            {
                var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var integers = numbers.Select(int.Parse).ToArray();
                
                var outliners = 0;

                //rostouci/klesajici
                var increasing = 0;
                for (int i = 1; i < integers.Length; i++)
                {
                    if (integers[i] > integers[0])
                    {
                        increasing++;
                    }
                }
                var ascending = increasing >= 2;

                var safe = ValidateRow(integers, ascending);


                if (safe)
                {
                    safeReports++;
                }
            }

            return safeReports;
        }

        private static bool ValidateRow(int[] integers, bool ascending)
        {
            var index = 1;
            var outliners = 0;
            for (int i = 1; i < integers.Length; i++)
            {
                var difference = integers[i] - integers[i - 1];

                if ((difference <= 0 || difference > 3) && ascending)
                {
                    index = i;
                    outliners++;
                    break;
                }
                else if ((difference >= 0 || difference < -3) && !ascending)
                {
                    index = i;
                    outliners++;
                    break;
                }
            }

            if (outliners == 1)
            {
                int[] array1 = integers.Where((_, idx) => idx != index).ToArray();
                for (int i = 1; i < array1.Length; i++)
                {
                    var difference = array1[i] - array1[i - 1];

                    if ((difference <= 0 || difference > 3) && ascending)
                    {
                        outliners++;
                        break;
                    }
                    else if ((difference >= 0 || difference < -3) && !ascending)
                    {
                        outliners++;
                        break;
                    }
                }

                if (outliners == 1)
                {
                    return true;
                }
            }

            outliners--;

            if (outliners == 1)
            {
                var array2 = integers.Where((_, idx) => idx != index-1).ToArray();
                for (int i = 1; i < array2.Length; i++)
                {
                    var difference = array2[i] - array2[i - 1];

                    if ((difference <= 0 || difference > 3) && ascending)
                    {
                        outliners++;
                        break;
                    }
                    else if ((difference >= 0 || difference < -3) && !ascending)
                    {
                        outliners++;
                        break;
                    }
                }
            }

            if (outliners == 2) 
            {
                return false;
            }

            return true;

        }

        private static double CalculatePart1(string[] lines)
        {
            var safeReports = 0;

            foreach (var line in lines)
            {
                var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var integers = numbers.Select(int.Parse).ToArray();

                if (integers[1] == integers[0])
                {
                    continue;
                }

                var ascending = integers[1] > integers[0];
                var safe = true;

                for (var i = 1; i < integers.Length; i++) 
                {
                    var difference = integers[i] - integers[i - 1];

                    if ((difference <= 0 || difference > 3)  && ascending)
                    {
                        safe = false;
                        break;
                    } else if ((difference >= 0 || difference < -3) && !ascending)
                    {
                        safe = false;
                        break;
                    }
                }

                if (safe) 
                {
                    safeReports++;
                }
            }

            return safeReports;
        }
    }
}
