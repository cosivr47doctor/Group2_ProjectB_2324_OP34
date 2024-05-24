using System.Text.Json.Serialization;

public class RoomModel : IModel
{
    [JsonPropertyName("ID")]
    public int Id {get; set;}

    [JsonPropertyName("Session id")]
    public int SessionId {get; set;}

    [JsonPropertyName("Row")]
    public int Row {get; set;}

    [JsonPropertyName("Taken seats")]
    public List<int> Seats {get; set;}


    public RoomModel(int id, int sessionId, int row, List<int> seats)
    {
        Id = id;
        SessionId = sessionId;
        Row = row;
        Seats = seats;
    }
}
