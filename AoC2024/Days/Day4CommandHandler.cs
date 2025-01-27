



internal class Day4CommandHandler
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
        var words = 0;
        var columns = lines[0].Length;
        var rows = lines.Length;
        var target = "MAS";
        var reversedTarget = "SAM";

        //for the whole grid
        for (int startRow = 0; startRow <= rows - 3; startRow++)
        {
            for (int startCol = 0; startCol <= columns - 3; startCol++)
            {
                // Extract diagonals of the current 3x3 subarray
                string topLeftToBottomRight = "";
                string topRightToBottomLeft = "";

                for (int i = 0; i < 3; i++)
                {
                    topLeftToBottomRight += lines[startRow + i][startCol + i];       // ↘ diagonal
                    topRightToBottomLeft += lines[startRow + i][startCol + 2 - i]; // ↙ diagonal
                }

                // Check for the pattern in both diagonals
                if ((topLeftToBottomRight.Contains(target) || topLeftToBottomRight.Contains(reversedTarget)) && (topRightToBottomLeft.Contains(target) || topRightToBottomLeft.Contains(reversedTarget))) words++;
            }
        }

        return words;
        }

    private static double CalculatePart1(string[] lines)
    {
        var words = 0;
        var columns = lines[0].Length;
        var rows = lines.Length;
        var target = "XMAS";
        var reversedTarget = "SAMX";

        //check horizontal
        foreach (var line in lines)
        {
            words += SearchAndCountWord(line, target);
            words += SearchAndCountWord(line, reversedTarget);
        }

        //check vertical
        for (int col = 0; col < columns; col++)
        {
            string vertical = "";

            for (int row = 0; row < rows; row++)
            {
                vertical += lines[row][col];
            }

            words += SearchAndCountWord(vertical, target);
            words += SearchAndCountWord(vertical, reversedTarget);
        }

        //Check Diagonal (top-left to bottom-right)
        words += CheckDiagonal(lines, words, columns, rows, target, reversedTarget);

        return words;

        static int CheckDiagonal(string[] lines, int words, int columns, int rows, string target, string reversedTarget)
        {
            for (int startRow = 0; startRow < rows; startRow++)
            {
                words += CountDiagonal(lines, target, startRow, 0, 1, 1);
                words += CountDiagonal(lines, reversedTarget, startRow, 0, 1, 1);
            }
            for (int startCol = 1; startCol < columns; startCol++)
            {
                words += CountDiagonal(lines, target, 0, startCol, 1, 1);
                words += CountDiagonal(lines, reversedTarget, 0, startCol, 1, 1);
            }

            // Search diagonally (top-right to bottom-left)
            for (int startRow = 0; startRow < rows; startRow++)
            {
                words += CountDiagonal(lines, target, startRow, columns - 1, 1, -1);
                words += CountDiagonal(lines, reversedTarget, startRow, columns - 1, 1, -1);
            }
            for (int startCol = columns - 2; startCol >= 0; startCol--)
            {
                words += CountDiagonal(lines, target, 0, startCol, 1, -1);
                words += CountDiagonal(lines, reversedTarget, 0, startCol, 1, -1);
            }

            return words;
        }
    }

    private static int SearchAndCountWord(string line, string target)
    {
        int count = 0;
        int index = line.IndexOf(target);

        while (index != -1)
        {
            count++;
            index = line.IndexOf(target, index + 1);
        }

        return count;
    }

    static int CountDiagonal(string[] grid, string target, int startRow, int startCol, int rowStep, int colStep)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int targetLength = target.Length;
        int count = 0;

        // Traverse the diagonal starting at (startRow, startCol)
        int row = startRow, col = startCol;
        string diagonalSequence = "";

        while (row >= 0 && row < rows && col >= 0 && col < cols)
        {
            diagonalSequence += grid[row][col];
            row += rowStep;
            col += colStep;
        }

        // Count occurrences of the target in the diagonal sequence
        count += SearchAndCountWord(diagonalSequence, target);

        return count;
    }
}