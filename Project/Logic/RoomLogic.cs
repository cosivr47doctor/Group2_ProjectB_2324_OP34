using System.Collections.Generic;
public class MoreRoomLogic
{
    private List<RoomModel> _room;
    public List<RoomModel> Room => _room;
    public MoreRoomLogic()
    {
        _room = GenericAccess<RoomModel>.LoadAll();
    }


    public List<RoomModel> SelectTakenSeats(int searchBy)
    {
        return _room.FindAll(roomS =>
            roomS.SessionId == searchBy);  // This is unhandy in case of duplicate sessions
    }
}