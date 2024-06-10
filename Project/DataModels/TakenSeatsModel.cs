using System.Text.Json.Serialization;

public class TakenSeatsModel : IModel
{
    [JsonPropertyName("ID")]
    public int Id {get; set;}

    [JsonPropertyName("reservationCode")]
    public string ReservationCode {get; set;}

    [JsonPropertyName("Session id")]
    public int SessionId {get; set;}

    [JsonPropertyName("Row")]
    public int Row {get; set;}

    [JsonPropertyName("Taken seats")]
    public List<int> Seats {get; set;}


    public TakenSeatsModel(int id, string reservationcode, int sessionId, int row, List<int> seats)
    {
        Id = id;
        ReservationCode = reservationcode;
        SessionId = sessionId;
        Row = row;
        Seats = seats;
    }
}
