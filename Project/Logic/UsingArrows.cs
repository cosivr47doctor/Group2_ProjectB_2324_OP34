public static class DisplayUtil
{
    private static string _decorator;
    public static string Decorator{
        get=>_decorator;
        set=> _decorator = value != "" ? $"{value}" : "\u001B[35m";
    }
    private static string _optionColor;
    public static string OptionColor{
        get=>_optionColor;
        set=>_optionColor = value != "" ? $"{value}" : "\u001B[32m";
    }
    public static int Display(List<string> options)
    {
        Console.Clear();
        Header.DisplayHeader();
        Console.CursorVisible = false;
        (int left, int top) = Console.GetCursorPosition();
        int selectedOption = 0;

        ConsoleKeyInfo key;
        bool isSelected = false;

        while (!isSelected)
        {
            Console.SetCursorPosition(left, top);

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{(selectedOption == i ? $"[x]{Decorator}" : "[ ]")}{options[i]}{OptionColor}");
            }

            key = Console.ReadKey(false);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = selectedOption == 0 ? options.Count - 1 : selectedOption - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedOption = selectedOption == options.Count - 1 ? 0 : selectedOption + 1;
                    break;

                case ConsoleKey.Enter:
                    isSelected = true;
                    break;

                    // case ConsoleKey.Escape:
                    //     selectedOption = options.Count - 1;
                    //     isSelected = true;
                    //     break;
            }
        }
        return selectedOption;
    }
}