static class ConsoleE
{
    public static string Input(string message)
    {
        Console.WriteLine(message);
        string userInput = Console.ReadLine();
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
