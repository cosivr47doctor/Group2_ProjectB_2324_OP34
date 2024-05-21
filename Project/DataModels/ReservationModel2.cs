using System.Text.Json.Serialization;

class ReservationModel2 : IModel
{
    [JsonPropertyName("Movie id")]
    public int Id {get; set;}

    [JsonPropertyName("seats")]
    public string Seats {get; set;}

    [JsonPropertyName("Food")]
    public string[] Food { get; set; }

    [JsonPropertyName("TotalPrice")]
    public decimal TotalPrice {get; set;}

    // [JsonPropertyName("Reservation code")]
    // public int ResvCode {get; set;}


    // public ReservationModel2(int id, string seats, decimal totalPrice)
    // {
    //     Id = id;
    //     Seats = seats;
    //     TotalPrice = totalPrice;
    // }

    public ReservationModel2(int id, string seats, string[] food, decimal totalprice)
    {
        Id = id;
        Seats = seats;
        Food = food;
        TotalPrice = totalprice;
    }
}
