public static class RoomSeats
{
    public static void SelectRoom(int accId, int sessionId, int movieId, int roomId)
    {
        if (roomId == 1) Room1(accId, sessionId, movieId);
        else if (roomId == 2) Room2(accId, sessionId, movieId);
    }
    public static (int, int, int) Room1(int accId, int sessionId, int movieId)
    {

        List<List<string>> seats = new List<List<string>>
        {
            new List<string>{"  ", "  ", " 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", "  ", "  "},
            new List<string>{"  ", " 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "  "},
            new List<string>{"  ", " 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "  "},
            new List<string>{" 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "11", "12"},
            new List<string>{" 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "11", "12"},
            new List<string>{" 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "11", "12"},
            new List<string>{" 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "11", "12"},
            new List<string>{" 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "11", "12"},
            new List<string>{" 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "11", "12"},
            new List<string>{" 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "11", "12"},
            new List<string>{" 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "11", "12"},
            new List<string>{"  ", " 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "  "},
            new List<string>{"  ", "  ", " 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", "  ", "  "},
            new List<string>{"  ", "  ", " 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", "  ", "  "}
        };
        int totalRows = seats.Count;
        string roomOne = "roomOne";

        (int selectedRow, int selectedColumn, int userAccId) = SelectSeats.DisplaySeats(accId, sessionId, movieId, seats, totalRows, roomOne);
        return SelectSeats.DisplaySeats(accId, sessionId, movieId, seats, totalRows, roomOne);
    }

    public static (int, int, int) Room2(int accId, int sessionId, int movieId)
    {
        Console.WriteLine("Room 2 doesn't exist yet"); Thread.Sleep(2500);
        throw new NotImplementedException();
    }

    public static (int, int, int) Room3(int accId, int sessionId, int movieId)
    {
        Console.WriteLine("Room 3 doesn't exist yet"); Thread.Sleep(2500);
        throw new NotImplementedException();
    }
}
