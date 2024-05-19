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
            new List<string>{" 1 ", " 2 ", " 3 ", " 4 ", " 5 ", " 6 ", " 7 ", " 8 ", " 9 ", " 10"},
            new List<string>{" 11", " 12", " 13", " 14", " 15", " 16", " 17", " 18", " 19", " 20"},
            new List<string>{" 21", " 22", " 23", " 24", " 25", " 26", " 27", " 28", " 29", " 30"},
            new List<string>{" 31", " 32", " 33", " 34", " 35", " 36", " 37", " 38", " 39", " 40"},
            new List<string>{" 41", " 42", " 43", " 44", " 45", " 46", " 47", " 48", " 49", " 50"},
            new List<string>{" 51", " 52", " 53", " 54", " 55", " 56", " 57", " 58", " 59", " 60"},
            new List<string>{" 61", " 62", " 63", " 64", " 65", " 66", " 67", " 68", " 69", " 70"},
            new List<string>{" 71", " 72", " 73", " 74", " 75", " 76", " 77", " 78", " 79", " 80"},
            new List<string>{" 81", " 82", " 83", " 84", " 85", " 86", " 87", " 88", " 89", " 90"},
            new List<string>{" 91", " 92", " 93", " 94", " 95", " 96", " 97", " 98", " 99", "100"}
        };

        (int selectedRow, int selectedColumn, int userAccId) = SelectSeats.DisplaySeats(accId, sessionId, movieId, seats);
        return SelectSeats.DisplaySeats(accId, sessionId, movieId, seats);
    }

    public static (int, int, int) Room2(int accId, int sessionId, int movieId)
    {
        throw new NotImplementedException();
    }
}
