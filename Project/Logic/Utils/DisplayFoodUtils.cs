public static class DisplayFoodUtil
{
    private static string _decorator;
    public static string Decorator
    {
        get => _decorator;
        set => _decorator = value != "" ? $"{value}" : "\u001B[35m";
    }

    private static string _optionColor;
    public static string OptionColor
    {
        get => _optionColor;
        set => _optionColor = value != "" ? $"{value}" : "\u001B[32m";
    }

    public static int DisplayF(List<string> options)
    {
        ConsoleE.Clear();
        Console.WriteLine($"Would you like to add food?");
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
                if (selectedOption == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Change color for the selected option
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White; // Reset color for other options
                }

                Console.WriteLine($"{Decorator}{options[i]}{OptionColor}");
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
