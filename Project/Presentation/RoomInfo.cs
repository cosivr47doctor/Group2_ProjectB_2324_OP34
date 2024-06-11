public static class RoomInfo
{
    public static (string, string, string) checkRoom(int roomNumber, MovieModel forMovieName, MovieScheduleModel forSessionDetails)
    {
        string date = forSessionDetails.Date.Date.ToString("yyyy-MM-dd");
        string sessionRoomDetails = $"Movie name: {forMovieName.Name}\nDate: {date}\nTime: {forSessionDetails.TimeIdPair.Keys.First()}";
        if (roomNumber == 1)
        {
            string roomPrint = $"\nRow                Room 1\n-----------------------------------------";
            string screen = "-----------------------------------------\n                   Screen";
            return (roomPrint, screen, sessionRoomDetails);
        }
        else if (roomNumber == 2)
        {
            string roomPrint = $"\nRow                          Room 2\n----------------------------------------------------------";
            string screen = "----------------------------------------------------------\n                             Screen";
            return (roomPrint, screen, sessionRoomDetails);
        }
        else if (roomNumber == 3)
        {
            string roomPrint = $"\nRow                                            Room 3\n----------------------------------------------------------------------------------------------";
            string screen = "----------------------------------------------------------------------------------------------\n                                               Screen";
            return (roomPrint, screen, sessionRoomDetails);
        }
        else
        {
            return(null, null, null);
        }
    }
    public static void RoomInformation(int roomNumber, MovieModel forMovieName, MovieScheduleModel forSessionDetails)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(checkRoom(roomNumber, forMovieName, forSessionDetails).Item2);
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