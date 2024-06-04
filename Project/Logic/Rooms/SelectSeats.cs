using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

public static class SelectSeats
{
    private static bool IsExpensiveSeat(int i, int j, string roomNumber)
    {
        if (roomNumber == "roomOne")
        {
            return (i == 3 && j >= 5 && j <= 6) || (i == 4 && j >= 4 && j <= 7) || (i == 10 && j >= 5 && j <= 6) || (i == 9 && j >= 4 && j <= 7) ||
            (i == 5 && j >= 3 && j <= 4) || (i == 6 && j >= 3 && j <= 4) || (i == 7 && j >= 3 && j <= 4) || (i == 8 && j >= 3 && j <= 4) ||
            (i == 5 && j >= 7 && j <= 8) || (i == 6 && j >= 7 && j <= 8) || (i == 7 && j >= 7 && j <= 8) || (i == 8 && j >= 7 && j <= 8);
        }
        else if (roomNumber == "roomTwo")
        {
            return false;
        }
        else if (roomNumber == "roomThree")
        {
            return false;
        }
        else 
        {
            return false;
        }
    }
        private static bool IsMoreExpensiveSeat(int i, int j, string roomNumber)
    {
        if (roomNumber == "roomOne")
        {
            return (i == 5 && j >= 5 && j <= 6) || (i == 6 && j >= 5 && j <= 6) || (i == 7 && j >= 5 && j <= 6) || (i == 8 && j >= 5 && j <= 6);
        }
        else if (roomNumber == "roomTwo")
        {
            return false;
        }
        else if (roomNumber == "roomThree")
        {
            return false;
        }
        else 
        {
            return false;
        }
    }
    public static (int, int, int) DisplaySeats(int accId, int sessionId, int movieId, List<List<string>> options, int seatsCount, string roomNumber)
    {
        Console.Clear();
        Console.CursorVisible = false;

        int selectedRow = 0;
        int selectedColumn = 0;

        ConsoleKeyInfo key;
        bool isSelected = false;
        List<int> seatsNumbers = new List<int>();
        List<string> seatsTaken = new List<string>();
        List<int> seatsTakenColumn = new List<int>();
        int enteredRow = 0;
        int correctRow = 0;

        while (!isSelected)
        {
            Console.SetCursorPosition(0, 0);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(RoomInfo.checkRoom(roomNumber).Item1);
            Console.ResetColor();

            for (int i = 0; i < seatsCount; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green; // Change color for the Row
                string rowLabel = (seatsCount - i).ToString().PadLeft(2, ' ');
                Console.Write($"{rowLabel} | ");
                Console.ResetColor();

                for (int j = 0; j < options[i].Count; j++)
                {
                    if (selectedRow == i && selectedColumn == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // Change color for the selected option
                    }
                    else if (options[i][j] == "XX")
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Change color for taken seats
                    }
                    else if (IsExpensiveSeat(i,j, roomNumber))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; // Change color for specified seats
                    }
                    else if (IsMoreExpensiveSeat(i,j, roomNumber))
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
                Console.WriteLine("");
            }
            RoomInfo.RoomInformation(roomNumber);

            key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedRow > 0 && options[selectedRow][selectedColumn] == "  ")
                    {
                        while (selectedRow > 0 && options[selectedRow - 1][selectedColumn] == "  ")
                        {
                            selectedRow--;
                        }
                    }
                    else if (selectedRow > 0 && options[selectedRow - 1][selectedColumn] != "  ")
                    {
                        selectedRow--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedRow < options.Count - 1 && options[selectedRow][selectedColumn] == "  ")
                    {
                        while (selectedRow < options.Count - 1 && options[selectedRow + 1][selectedColumn] == "  ")
                        {
                            selectedRow++;
                        }
                    }
                    if (selectedRow < options.Count - 1 && options[selectedRow + 1][selectedColumn] != "  ")
                    {
                        selectedRow++;
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (selectedColumn > 0 && options[selectedRow][selectedColumn] == "  ")
                    {
                        while (selectedColumn > 0 && options[selectedRow][selectedColumn - 1] == "  ")
                        {
                            selectedColumn--;
                        }
                    }
                    else if (selectedColumn > 0 && options[selectedRow][selectedColumn - 1] != "  ")
                    {
                        selectedColumn--;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (selectedColumn < options[selectedRow].Count - 1 && options[selectedRow][selectedColumn] == "  ")
                    {
                        while (selectedColumn < options[selectedRow].Count - 1 && options[selectedRow][selectedColumn + 1] == "  ")
                        {
                            selectedColumn++;
                        }   
                    }
                    if (selectedColumn < options[selectedRow].Count - 1 && options[selectedRow][selectedColumn + 1] != "  ")
                    {
                        selectedColumn++;
                    }
                    break;


                case ConsoleKey.Enter:
                    if (options[selectedRow][selectedColumn] == "  ")
                    {
                        Console.WriteLine("\nNo seat selected.");
                    }
                    else if (options[selectedRow][selectedColumn] == "XX")
                    {
                        Console.WriteLine("\nSeat already taken.");
                    }
                    else
                    {
                        if (seatsNumbers.Count >= 8)
                        {
                            Console.WriteLine("\n\nMax amount of seats reached");
                        }
                        else if (seatsNumbers.Count >= 1)
                        {
                            // Check if the selected row is the same as the entered row
                            if (enteredRow != selectedRow)
                            {
                                Console.WriteLine("\n\n\nCan only select seats next to each other");
                                break;
                            }

                            string seatSelected = options[selectedRow][selectedColumn];
                            int SeatNum = int.Parse(seatSelected);
                            seatsNumbers.Sort();
                            if (seatsNumbers[0] - 1 == SeatNum || seatsNumbers[^1] + 1 == SeatNum)
                            {
                                string stringRow = correctRow.ToString();
                                seatsTaken.Add(seatSelected);
                                seatsTakenColumn.Add(selectedColumn);
                                seatsNumbers.Add(SeatNum);
                                options[selectedRow][selectedColumn] = "XX"; // Mark the seat as taken
                                seatsNumbers.Sort();
                                Console.WriteLine($"Selected seats: {string.Join(", ", seatsNumbers)} on row: {stringRow}");
                            }
                            else
                            {
                                Console.WriteLine("\n\n\nCan only select seats next to each other");
                            }
                        }
                        else
                        {
                            enteredRow = selectedRow; // Store the entered row when the first seat is selected
                            correctRow = seatsCount - enteredRow;
                            string stringRow = correctRow.ToString();
                            string seatSelected = options[selectedRow][selectedColumn];
                            seatsTaken.Add(seatSelected);
                            seatsTakenColumn.Add(selectedColumn);
                            int SeatNum = int.Parse(seatSelected);
                            seatsNumbers.Add(SeatNum);
                            options[selectedRow][selectedColumn] = "XX"; // Mark the seat as taken
                            Console.WriteLine($"Selected seat: {string.Join(", ", seatsNumbers)} on row: {stringRow}");
                        }
                    }
                    break;


                case ConsoleKey.Spacebar:
                    if (seatsNumbers.Count <= 0)
                    {
                        Console.WriteLine("No seats selected");
                    }
                    else
                    {
                        string stringRow = correctRow.ToString();
                        seatsNumbers.Sort();
                        Console.WriteLine($"Seat(s) selected successfully: {string.Join(", ", seatsNumbers)} on row: {stringRow}");
                        Console.WriteLine("Seats confirmed");
                        Thread.Sleep(3000);
                        string seatsString = $"row: {stringRow}, seats: {string.Join(",", seatsNumbers)}";

                        int price = 0;
                        foreach (var seatColumn in seatsTakenColumn)
                        {
                            if (IsMoreExpensiveSeat(enteredRow, seatColumn, roomNumber))
                            {
                                price += 16;
                            }
                            else if (IsExpensiveSeat(enteredRow, seatColumn, roomNumber))
                            {
                                price += 14;
                            }
                            else
                            {
                                price += 12;
                            }
                        }

                        string reservationCode = GenerateResvCode.GenerateCode();
                        RoomModel roomDetails = new RoomModel(0, reservationCode, sessionId, enteredRow, seatsTakenColumn);
                        AddReservation.AskForFood(accId, sessionId, movieId, seatsString, price, roomDetails);
                    }
                    break;

                case ConsoleKey.Backspace:
                    Console.WriteLine("Heading back to the start menu");
                    break;
    
                case ConsoleKey.Escape:
                    UserMenu.Start(accId);
                    break;
            }
        }
        Console.ResetColor();
        return (selectedRow, selectedColumn, accId);
    }
}
