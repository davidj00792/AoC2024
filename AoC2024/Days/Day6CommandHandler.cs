

using AoC2024.Days;
using System.ComponentModel;

internal class Day6CommandHandler
{

    internal static int Execute(string destination, string? part)
    {
        var lines = File.ReadAllLines(destination);
        var final = 0;

        switch (part)
        {
            case "1":
                final = CalculatePart1(lines);
                break;
            case "2":
                //final = CalculatePart2(lines); 
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

    private static int CalculatePart1(string[] lines)
    {
        var guardPosition = FindGuard(lines);

        var patrolRoute = PatrolRoute(lines, guardPosition);

        return patrolRoute;
    }

    private static Guard FindGuard(string[] lines)
    {
        var lineLength = lines[0].Length;
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lineLength; j++)
            {
                if (lines[i][j] == '^')
                {
                    return new Guard { X = j, Y = i , Direction = '^', Steps = 1 };
                }
            }
        }

        throw new Exception("Guard not found");
    }

    private static int PatrolRoute(string[] lines, Guard guardPosition)
    {
        var startPosition = new Guard { X = guardPosition.X, Y = guardPosition.Y};

        guardPosition.Y -= 1;

        var continuePatrol = true;
        var visited = new HashSet<(int, int)>
        {
            (startPosition.X, startPosition.Y)
        };
        //bez nakonec a pocitejs

        while (continuePatrol)
        {
            continuePatrol = Travel(lines, guardPosition, startPosition, visited);
        }

        //zatoc doprava a bez nakonec a pocitej, dokud se nepotkas se startovnim mistem
        return guardPosition.Steps +1;
    }

    private static bool Travel(string[] lines, Guard guardPosition, Guard startPosition, HashSet<(int, int)> visited)
    {
        switch (guardPosition.Direction)
        {
            case '^':
                return TravelUp(lines, guardPosition, startPosition, visited);
            case '>':
                return TravelRight(lines, guardPosition, startPosition, visited);
            case '<':
                return TravelLeft(lines, guardPosition, startPosition, visited);
            case 'ˇ':
                return TravelDown(lines, guardPosition, startPosition, visited);
        }

        throw new Exception("Indalid direction");
    }

    private static bool TravelDown(string[] lines, Guard guardPosition, Guard startPosition, HashSet<(int, int)> visited)
    {
        var linesCount = lines.Length - 1;

        while (guardPosition.Y != linesCount)
        {
            if (lines[guardPosition.Y+1][guardPosition.X] == '#') { break; }

            if (visited.Contains((guardPosition.X, guardPosition.Y)))
            {
                guardPosition.Y++;
            }
            else
            {
                guardPosition.Y++;
                visited.Add((guardPosition.X, guardPosition.Y-1));
                guardPosition.Steps++;
            }
        }

        if (guardPosition.Y + 1 > linesCount) { return false; }

        guardPosition.Direction = '<';

        return true;
    }

    private static bool TravelLeft(string[] lines, Guard guardPosition, Guard startPosition, HashSet<(int, int)> visited)
    {
        while (guardPosition.X != 0)
        {
            if (lines[guardPosition.Y][guardPosition.X-1] == '#') { break; }

            if (visited.Contains((guardPosition.X, guardPosition.Y)))
            {
                guardPosition.X--;
            }
            else
            {
                guardPosition.X--;
                visited.Add((guardPosition.X+1, guardPosition.Y));
                guardPosition.Steps++;
            }
        }

        if (guardPosition.X - 1 < 0) { return false; }

        guardPosition.Direction = '^';

        return true;
    }

    private static bool TravelRight(string[] lines, Guard guardPosition, Guard startPosition, HashSet<(int, int)> visited)
    {
        var lineLength = lines[0].Length - 1;

        while (guardPosition.X != lineLength)
        {
            if (lines[guardPosition.Y][guardPosition.X+1] == '#') { break; }

            if (visited.Contains((guardPosition.X, guardPosition.Y)))
            {
                guardPosition.X++;
            }
            else
            {
                guardPosition.X++;
                visited.Add((guardPosition.X-1, guardPosition.Y));
                guardPosition.Steps++;
            }
        }

        if (guardPosition.X + 1 > lineLength) { return false; }

        guardPosition.Direction = 'ˇ';

        return true;
    }

    private static bool TravelUp(string[] lines, Guard guardPosition, Guard startPosition, HashSet<(int, int)> visited)
    {
        while (guardPosition.Y != 0)
        {
            if (lines[guardPosition.Y-1][guardPosition.X] == '#') { break; }

            if (visited.Contains((guardPosition.X, guardPosition.Y)))
            {
                guardPosition.Y--;
            }
            else
            {
                guardPosition.Y--;
                visited.Add((guardPosition.X, guardPosition.Y+1));
                guardPosition.Steps++;
            }
        }

        if (guardPosition.Y - 1 < 0) { return false; }

        guardPosition.Direction = '>';

        return true;
    }
}

internal class Guard
{
    public int X { get; set; }
    public int Y { get; set; }

    public int Steps { get; set; }

    public char Direction { get; set; }
}