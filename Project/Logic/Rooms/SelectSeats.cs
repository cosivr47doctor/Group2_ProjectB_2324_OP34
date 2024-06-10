using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

public static class SelectSeats
{
    public static (int, int, int) DisplaySeats(int accId, int sessionId, int movieId, List<List<string>> options, int seatsCount, int roomNumber)
    {
        Console.Clear();
        Console.CursorVisible = false;

        int selectedRow = 0;
        int selectedColumn = 4;

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


            MovieLogic objMovieLogic = new MovieLogic();
            MovieModel selectedMovieName = objMovieLogic.GetBySearch(movieId);

            RoomLogic objRoomLogic = new RoomLogic();
            MovieScheduleModel getSessionDetails = objRoomLogic.selectSessionDetails(sessionId);

            Console.WriteLine($"{RoomInfo.checkRoom(roomNumber, selectedMovieName, getSessionDetails).Item3}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{RoomInfo.checkRoom(roomNumber, selectedMovieName, getSessionDetails).Item1}");
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
                    else if (RoomSeats.deluxeSeats(i,j, roomNumber))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; // Change color for specified seats
                    }
                    else if (RoomSeats.vipSeats(i,j, roomNumber))
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
            RoomInfo.RoomInformation(roomNumber, selectedMovieName, getSessionDetails);

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

                        RoomModel selectPrice = objRoomLogic.selectRoomfromJson(roomNumber);
                        decimal price = 0;
                        foreach (var seatColumn in seatsTakenColumn)
                        {
                            if (RoomSeats.deluxeSeats(enteredRow, seatColumn, roomNumber))
                            {
                                price += selectPrice.Price2;
                            }
                            else if (RoomSeats.vipSeats(enteredRow, seatColumn, roomNumber))
                            {
                                price += selectPrice.Price3;
                            }
                            else
                            {
                                price += selectPrice.Price1;
                            }
                        }

                        string reservationCode = GenerateResvCode.GenerateCode();
                        TakenSeatsModel roomDetails = new TakenSeatsModel(0, reservationCode, sessionId, enteredRow, seatsTakenColumn);
                        AddReservation.AskForFood(accId, sessionId, movieId, seatsString, price, roomDetails);
                    }
                    break;

                case ConsoleKey.Backspace:
                    Console.WriteLine("I don't think you have the facilities for that big man");
                    break;
    
                case ConsoleKey.Escape:
                    UserMenu.Start(accId);
                    break;
            }
        }
        Console.ForegroundColor = ConsoleColor.White;
        return (selectedRow, selectedColumn, accId);
    }
}
