using System.Runtime.Intrinsics.Arm;

static class ConsoleE
{
    public static string[] backContains = new string[] {"q", "Q", "[q]", "[Q]", "go back"};
    public static string Input(string message = "Press enter to go back", bool hideInput = false)
    {
        string userInput = "";
        if (!hideInput)
        {
            Console.WriteLine(message);
            userInput = Console.ReadLine();
        }

        else
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                Console.Write("");
            }
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
        }
        return userInput;
    }

    public static int? IntInput(string message)
    {
        Console.WriteLine(message);
        int intParsed;
        bool intParsedString = int.TryParse(Console.ReadLine(), out intParsed);
        if (intParsedString) {return intParsed;}
        Console.WriteLine("Invalid input: not numeric");
        return null;
    }

    public static bool BackContains(string message)
    {
        if (backContains.Contains(message)) return true;
        return false;
    }
}
