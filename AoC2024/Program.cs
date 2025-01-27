using AoC2024.Days;

const string dataFile = "C:\\Users\\xjanc\\source\\repos\\AoC2024\\Data.txt";
const string testFile = "C:\\Users\\xjanc\\source\\repos\\AoC2024\\Test.txt";

string command = string.Empty;
bool isValidCommand = false;

while (!isValidCommand)
{
    Console.WriteLine("Please enter a file to use:");
    command = Console.ReadLine();

    // Validate the command
    if (IsValidCommand(command))
    {
        isValidCommand = true;
        Console.WriteLine("Command accepted: " + command);
    }
    else
    {
        Console.WriteLine("Invalid command. Please try again.");
    }
}

if (string.Equals(command, "exit"))
{
    Console.WriteLine("Exiting...");
    return;
} 
else
{
    var destination = GetDestination(command);

    if (File.Exists(destination))
    {
        Launch(destination);
    }
    else
    {
        Console.WriteLine("File does not exist. Exiting...");
    }
}

string GetDestination(string? command)
{
    if (string.Equals(command, "test"))
    {
        return testFile;
    }

    if (string.Equals(command, "data"))
    {
        return dataFile;
    }

    return "Invalid file";
}

bool IsValidCommand(string? command)
{
    var commands = new List<string>()
    {
        "test", "data", "exit"
    };
    
    return commands.Contains(command);
}

void Launch(string destination)
{
    Console.WriteLine("Select day to launch:");
    var day = Console.ReadLine();

    Console.WriteLine("Select part to launch:");
    var part = Console.ReadLine();

    double result = 0;
    switch (day)
    {
        case "1":
            result = Day1CommandHandler.Execute(destination, part);
            Console.WriteLine("Success");
            break;
        case "2":
            result = Day2CommandHandler.Execute(destination, part);
            Console.WriteLine("Success");
            break;
        case "3":
            result = Day3CommandHandler.Execute(destination, part);
            Console.WriteLine("Success");
            break;
        case "4":
            result = Day4CommandHandler.Execute(destination, part);
            Console.WriteLine("Success");
            break;
        case "5":
            result = Day5CommandHandler.Execute(destination, part);
            Console.WriteLine("Success");
            break;
        case "6":
            result = Day6CommandHandler.Execute(destination, part);
            Console.WriteLine("Success");
            break;
        case "7":
            result = Day7CommandHandler.Execute(destination, part);
            Console.WriteLine("Success");
            break;
        default:
            Console.WriteLine("Invalid day.");
            break;
    } 

    Console.WriteLine($"Final result is: {result}");
}