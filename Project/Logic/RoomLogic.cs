using System.Collections.Generic;
public class RoomLogic
{
    private List<RoomModel> _room;
    public List<RoomModel> Room => _room;

    private List<MovieScheduleModel> _schedule;
    public List<MovieScheduleModel> Schedule => _schedule;

    private List<TakenSeatsModel> _takenSeats;
    public List<TakenSeatsModel> TakenSeats => _takenSeats;
    public RoomLogic()
    {
        _room = GenericAccess<RoomModel>.LoadAll();
        _schedule = GenericAccess<MovieScheduleModel>.LoadAll();
        _takenSeats = GenericAccess<TakenSeatsModel>.LoadAll();
    }


    public List<TakenSeatsModel> SelectTakenSeats(int searchBy)
    {
        return _takenSeats.FindAll(takenSeatS =>
            takenSeatS.SessionId == searchBy);  // This is unhandy in case of duplicate sessions
    }
    public MovieScheduleModel selectSessionDetails(int sessionId) => _schedule.Find(schedule => schedule.Id == sessionId);
    public RoomModel selectRoomfromJson(int roomId) => _room.Find(roomS => roomS.Id == roomId);
}