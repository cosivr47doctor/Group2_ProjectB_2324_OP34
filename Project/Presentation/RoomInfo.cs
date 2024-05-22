public static class RoomInfo
{
    public static void RoomInformation()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("-----------------------------------------\n                   Screen");
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