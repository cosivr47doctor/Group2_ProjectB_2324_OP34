public static class RoomSeats
{
    public static void SelectRoom(int accId, int sessionId, int movieId, int roomId)
    {
        RoomLogic roomLogic = new RoomLogic();
        List<TakenSeatsModel> takenSeats = roomLogic.SelectTakenSeats(sessionId);

        RoomModel getRoom = roomLogic.selectRoomfromJson(roomId);
        List<List<string>> roomLayout = getRoom.Seats;

        Room(accId, sessionId, movieId, takenSeats, roomId, roomLayout);
    }

    public static (int, int, int) Room(int accId, int sessionId, int movieId, List<TakenSeatsModel> takenSeats, int roomNum, List<List<string>> roomLayout)
    {
        foreach (var room in takenSeats)
        {
            int row = room.Row;
            foreach (var seat in room.Seats)
            {
                roomLayout[row][seat] = "XX";
            }
        }
            
        int totalRows = roomLayout.Count;;

        (int selectedRow, int selectedColumn, int userAccId) = SelectSeats.DisplaySeats(accId, sessionId, movieId, roomLayout, totalRows, roomNum);
        return SelectSeats.DisplaySeats(accId, sessionId, movieId, roomLayout, totalRows, roomNum);
    }
    public static bool deluxeSeats(int i, int j, int roomNumber)
    {
        if (roomNumber == 1)
        {
            return (i == 3 && j >= 5 && j <= 6) || (i == 4 && j >= 4 && j <= 7) || (i == 10 && j >= 5 && j <= 6) || (i == 9 && j >= 4 && j <= 7) ||
            (i == 5 && j >= 3 && j <= 4) || (i == 6 && j >= 3 && j <= 4) || (i == 7 && j >= 3 && j <= 4) || (i == 8 && j >= 3 && j <= 4) ||
            (i == 5 && j >= 7 && j <= 8) || (i == 6 && j >= 7 && j <= 8) || (i == 7 && j >= 7 && j <= 8) || (i == 8 && j >= 7 && j <= 8);
        }
        else if (roomNumber == 2)
        {
            return (i == 1 && j >= 6 && j <= 11) || (i == 2 && j >= 5 && j <= 12) || (i == 3 && j >= 5 && j <= 12) || (i == 4 && j >= 4 && j <= 13) ||
            (i == 13 && j >= 5 && j <= 12) || (i == 14 && j >= 6 && j <= 11) || (i == 15 && j >= 6 && j <= 11) || (i == 5 && j >= 4 && j <= 7) ||
            (i == 5 && j >= 10 && j <= 13) || (i == 6 && j >= 3 && j <= 6) || (i == 6 && j >= 11 && j <= 14) || (i == 7 && j >= 3 && j <= 6) ||
            (i == 7 && j >= 11 && j <= 14) || (i == 8 && j >= 2 && j <= 5) || (i == 8 && j >= 12 && j <= 15) || (i == 9 && j >= 2 && j <= 5) ||
            (i == 9 && j >= 12 && j <= 15) || (i == 10 && j >= 2 && j <= 5) || (i == 10 && j >= 12 && j <= 15) || (i == 11 && j >= 3 && j <= 6) ||
            (i == 11 && j >= 11 && j <= 14) || (i == 12 && j >= 4 && j <= 7) || (i == 12 && j >= 10 && j <= 13);
        }
        else if (roomNumber == 3)
        {
            return (i == 1 && j >= 9 && j <= 20) || (i == 2 && j >= 8 && j <= 21) || (i == 3 && j >= 8 && j <= 21) || (i == 13 && j >= 8 && j <= 21) ||
            (i == 14 && j >= 9 && j <= 20) || (i == 15 && j >= 10 && j <= 19) || (i == 16 && j >= 12 && j <= 17) || (i == 4 && j >= 7 && j <= 12) ||
            (i == 4 && j >= 17 && j <= 21) || (i == 5 && j >= 7 && j <= 11) || (i == 5 && j >= 18 && j <= 22) || (i == 6 && j >= 6 && j <= 10) ||
            (i == 6 && j >= 19 && j <= 23) || (i == 7 && j >= 6 && j <= 10) || (i == 7 && j >= 19 && j <= 23) || (i == 8 && j >= 5 && j <= 10) ||
            (i == 8 && j >= 19 && j <= 24) || (i == 9 && j >= 5 && j <= 10) || (i == 9 && j >= 19 && j <= 24) || (i == 10 && j >= 6 && j <= 10) ||
            (i == 10 && j >= 19 && j <= 23) || (i == 11 && j >= 7 && j <= 10) || (i == 11 && j >= 19 && j <= 22) || (i == 12 && j >= 8 && j <= 12) ||
            (i == 12 && j >= 17 && j <= 21);
        }
        else 
        {
            return false;
        }
    }
    public static bool vipSeats(int i, int j, int roomNumber)
    {
        if (roomNumber == 1)
        {
            return (i == 5 && j >= 5 && j <= 6) || (i == 6 && j >= 5 && j <= 6) || (i == 7 && j >= 5 && j <= 6) || (i == 8 && j >= 5 && j <= 6);
        }
        else if (roomNumber == 2)
        {
            return (i == 5 && j >= 8 && j <= 9) || (i == 6 && j >= 7 && j <= 10) || (i == 7 && j >= 6 && j <= 11) || (i == 8 && j >= 6 && j <= 11) || 
            (i == 9 && j >= 6 && j <= 11) || (i == 10 && j >= 6 && j <= 11) || (i == 11 && j >= 7 && j <= 10) || (i == 12 && j >= 8 && j <= 9);
        }
        else if (roomNumber == 3)
        {
            return (i == 4 && j >= 13 && j <= 16) || (i == 5 && j >= 12 && j <= 17) || (i == 6 && j >= 11 && j <= 18) || (i == 6 && j >= 11 && j <= 18) ||
            (i == 7 && j >= 11 && j <= 18) || (i == 8 && j >= 11 && j <= 18) || (i == 9 && j >= 11 && j <= 18) || (i == 10 && j >= 11 && j <= 18) ||
            (i == 11 && j >= 11 && j <= 18) || (i == 12 && j >= 13 && j <= 16);
        }
        else 
        {
            return false;
        }
    }
}