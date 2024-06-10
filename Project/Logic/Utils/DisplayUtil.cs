public static class DisplayUtil
{
    public static int MenuDisplay(List<string> options)
    {
        ConsoleE.Clear();
        Header.DisplayHeader();
        return Display(options);
    }

    public static int DisplayAddFood(List<string> options)
    {
        ConsoleE.Clear();
        Console.WriteLine($"Would you like to add food?");
        return Display(options);
    }


    public static int Display(List<string> options)
    {
        ConsoleE.CursorVisible(false);    // Console.CursorVisible = false;
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

                Console.WriteLine($"{options[i]}");
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
        Console.ForegroundColor = ConsoleColor.White;
        return selectedOption;
    }
}
