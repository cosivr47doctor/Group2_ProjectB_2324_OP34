using System;

public class PasswordInput
{
    public static string InputPassword()
    {
        Console.WriteLine("Enter password: ");
        // the password should be asterisks in the console
        string password = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            // ignore any key out of range (numbers, letters, and special characters are allowed)
            List<char> special_characters = new List<char> { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '+', '=', '{', '}', '[', ']', '|', '\\', ':', ';', '"', '\'', '<', '>', ',', '.', '?', '/', '~', '`' };
            if (((key.KeyChar >= 'a' && key.KeyChar <= 'z') || (key.KeyChar >= 'A' && key.KeyChar <= 'Z') || (key.KeyChar >= '0' && key.KeyChar <= '9') || special_characters.Contains(key.KeyChar)) && key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            // backspace should be handled
            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }

            // stop when Enter is pressed
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return password;
    }
}
