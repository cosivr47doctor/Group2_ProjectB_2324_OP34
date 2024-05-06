using System.Runtime.Intrinsics.Arm;

static class ConsoleE
{
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
        bool intParsedString = !int.TryParse(Console.ReadLine(), out intParsed);
        if (intParsedString) return intParsed;
        Console.WriteLine("Invalid input");
        return null;
    }
}
