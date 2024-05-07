public static class RoomSeats
{
    public static (int, int) Room1()
    {

        List<List<string>> seats = new List<List<string>>
        {
            new List<string>{"1 ", "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10"},
            new List<string>{"11", "12", "13", "14", "15", "16", "17", "18", "19", "20"},
            new List<string>{"21", "22", "23", "24", "25", "26", "27", "28", "29", "30"},
            new List<string>{"31", "32", "33", "34", "35", "36", "37", "38", "39", "40"},
            new List<string>{"41", "42", "43", "44", "45", "46", "47", "48", "49", "50"}
        };

        (int selectedRow, int selectedColumn) = SelectSeats.DisplaySeats(seats);
        return SelectSeats.DisplaySeats(seats);
    }
}
