using System;
using System.Collections.Generic;

public static class SelectSeats
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

public static (int, int) DisplaySeats(List<List<string>> options)
{
    Console.Clear();
    Console.CursorVisible = false;

    int selectedRow = 0;
    int selectedColumn = 0;

    ConsoleKeyInfo key;
    bool isSelected = false;
    List<string> seats = new List<string>();
    List<int> seatsNumbers = new List<int>();

    while (!isSelected)
    {
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
        Console.WriteLine("           Screen");
        Console.WriteLine("-----------------------------");
        for (int i = 0; i < options.Count; i++)
        {
            for (int j = 0; j < options[i].Count; j++)
            {
                if (selectedRow == i && selectedColumn == j)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Change color for the selected option
                }
                else if (options[i][j] == " X ")
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Change color for taken seats
                }
                else if ((i == 2 && j >= 3 && j <= 6) || (i == 3 && j >= 2 && j <= 7) || (i == 6 && j >= 2 && j <= 7) || (i == 7 && j >= 3 && j <= 6) || (i == 4 && j >= 2 && j <= 2) || (i == 5 && j >= 2 && j <= 2) || (i == 5 && j >= 7 && j <= 7) || (i == 4 && j >= 7 && j <= 7))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Change color for specified seats
                }
                else if ((i == 4 && j >= 3 && j <= 6) || (i == 5 && j >= 3 && j <= 6))
                {
                    Console.ForegroundColor = ConsoleColor.Blue; // Change color for specified seats (more expensive)
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White; // Reset color for other options
                }

                Console.Write($"{options[i][j]} ");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
        Console.WriteLine("-----------------------------");
        Console.WriteLine("");
        Console.WriteLine("X = Seat already taken");
        Console.WriteLine("");
        Console.WriteLine("Price per ticket: 12,-");
        Console.WriteLine("Amount of seats: 1-8");
        Console.WriteLine("");
        Console.WriteLine("Press [Enter] to Select seat  ");
        //Console.WriteLine("Press [Backspace] to Cancel reservation  ");
        Console.WriteLine("Press [Spacebar] to Confirm seats ");
        Console.WriteLine("Press [Escape] to Cancel reservation  ");
        Console.WriteLine("");

        key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                selectedRow = Math.Max(0, selectedRow - 1);
                break;

            case ConsoleKey.DownArrow:
                selectedRow = Math.Min(options.Count - 1, selectedRow + 1);
                break;

            case ConsoleKey.LeftArrow:
                selectedColumn = Math.Max(0, selectedColumn - 1);
                break;

            case ConsoleKey.RightArrow:
                selectedColumn = Math.Min(options[selectedRow].Count - 1, selectedColumn + 1);
                break;

            case ConsoleKey.Enter:
                if (options[selectedRow][selectedColumn] == " X ")
                {
                    Console.WriteLine("Seat already taken.");
                }
                else
                {
                    if (seats.Count >= 8)
                    {
                        Console.WriteLine("Max amount of seats reached");
                    }
                    else if (seatsNumbers.Count >= 1)
                    {
                        string seatSelected = options[selectedRow][selectedColumn];
                        int SeatNum = int.Parse(seatSelected);
                        seatsNumbers.Sort();
                            if (seatsNumbers[0] - 1 == SeatNum || seatsNumbers[^1] + 1 == SeatNum)
                            {
                                seatsNumbers.Add(SeatNum);
                                seats.Add(seatSelected);
                                options[selectedRow][selectedColumn] = " X "; // Mark the seat as taken

                            }
                            else
                            {
                                Console.WriteLine("Can only select seats next to each other");
                            }
                    }
                    else
                    {
                    string seatSelected = options[selectedRow][selectedColumn];
                    int SeatNum = int.Parse(seatSelected);
                    seatsNumbers.Add(SeatNum);
                    seats.Add(seatSelected);
                    options[selectedRow][selectedColumn] = " X "; // Mark the seat as taken
                    }
                }
                break;
                case ConsoleKey.Spacebar:
                {
                    if (seats.Count <= 0)
                    {
                        Console.WriteLine("No seats selected");
                    }
                    else
                    {
                    Console.WriteLine($"Seat(s) selected successfully: {string.Join(", ", seats)}");
                    }
                    break;
                    
                }
                case ConsoleKey.Backspace:
                {
                    Console.WriteLine("Heading back to the start menu");
                    
                    break;
                }
                case ConsoleKey.Escape:
                {
                    Console.WriteLine("Heading back to the start menu");
                    break;
                }
        }
    }

    Console.ResetColor();

    return (selectedRow, selectedColumn);
}


}