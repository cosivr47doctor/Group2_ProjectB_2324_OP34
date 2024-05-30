public static class RoomInfo
{
    public static (string, string) checkRoom(string roomNumber)
    {
            if (roomNumber == "roomOne")
            {
                string roomPrint = "Row                Room 1\n-----------------------------------------";
                string screen = "-----------------------------------------\n                   Screen";
                return (roomPrint, screen);
            }
            else if (roomNumber == "roomTwo")
            {
                string roomPrint = "Row                          Room 2\n----------------------------------------------------------";
                string screen = "----------------------------------------------------------\n                   Screen";
                return (roomPrint, screen);
            }
            else if (roomNumber == "roomThree")
            {
                string roomPrint = "Row                                            Room 3\n------------------------------------------------------------------------------------------";
                string screen = "------------------------------------------------------------------------------------------\n                   Screen";
                return (roomPrint, screen);
            }
            else
            {
                return(null, null);
            }
    }
    public static void RoomInformation(string roomNumber)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(checkRoom(roomNumber).Item2);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nX = Seat already taken");
        Console.ResetColor();
        Console.WriteLine("\nWhite = 12,-");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Yellow = 14,-");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Blue = 16,-");
        Console.ResetColor();
        Console.WriteLine("\nAmount of seats: 1-8\n");
        Console.WriteLine("Press [Enter] to Select seat");
        //Console.WriteLine("Press [Backspace] to Cancel reservation");
        Console.WriteLine("Press [Spacebar] to Confirm seats");
        Console.WriteLine("Press [Escape] to Cancel reservation\n");
    }
}