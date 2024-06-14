using System.Runtime.Intrinsics.Arm;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Cryptography;

public static class ConsoleE
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
    
    /*
    public static int IntInput(int? parsedInt)
    {
        int convertedInt = (int)parsedInt;
        return convertedInt;
    }
    */
    
    /*
    public static int IntInput(string message)
    {
        Console.WriteLine(message);
        int intParsed;
        bool intParsedString = int.TryParse(Console.ReadLine(), out intParsed);
        if (intParsedString) return intParsed;
        Console.WriteLine("Invalid input: not numeric");
        return Int32.MinValue;
    }
    */
    public static bool IsNullOrEmptyOrWhiteSpace<T>(T inp)
    {
        bool isNullOrEmptyOrWhiteSpace = false;
        if (inp is string)
        {
            string str = Convert.ToString(inp);
            if (string.IsNullOrEmpty(str)) isNullOrEmptyOrWhiteSpace = true;
            if (string.IsNullOrWhiteSpace(str)) isNullOrEmptyOrWhiteSpace = true;
        }
        else
        {
            if (inp == null) isNullOrEmptyOrWhiteSpace = true;
            if (inp is IModel)
            {
                IModel imodel = (IModel)inp;
                if (imodel.Id == null && imodel.Id == -1) isNullOrEmptyOrWhiteSpace = true;
            }
        }

        return isNullOrEmptyOrWhiteSpace;
    }

    public static bool BackContains(string message)
    {
        if (backContains.Contains(message)) return true;
        return false;
    }

    public static void Clear()
    {
        if (!Debugger.IsAttached)
        Console.Clear();
    }

    public static void CursorVisible(bool visible)
    {
        if (!visible)
        {
            if (!Debugger.IsAttached) Console.CursorVisible = false;
        }
        else
        {
            if (!Debugger.IsAttached) Console.CursorVisible = true;
        }
    }
    public static bool IsDigitsOnly(string str)
    {
        if (IsNullOrEmptyOrWhiteSpace(str)) return false;
        else if (str.Length > 1 && str[0] == '0') return false;
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }
        return true;
    }
}
