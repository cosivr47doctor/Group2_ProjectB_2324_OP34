public static class RoomSeats
{
    public static (int, int, int) Room1(int foundMovie, int accId)
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

        (int selectedRow, int selectedColumn, int userAccId) = SelectSeats.DisplaySeats(seats, foundMovie, accId);
        return SelectSeats.DisplaySeats(seats, foundMovie, accId);
    }
}
