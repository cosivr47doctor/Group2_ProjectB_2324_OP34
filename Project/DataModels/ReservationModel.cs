using System.Text.Json.Serialization;

class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id {get; set;}

    // [JsonPropertyName("movieObject")]
    // public MovieModel ObjMovieModel {get; set;}

    [JsonPropertyName("name")]
    public string Name {get; set;}

    [JsonPropertyName("ticketsCount")]
    public int TicketsCount {get; set;}

    [JsonPropertyName("price")]
    public decimal Price {get; set;}

    [JsonPropertyName("seats")]
    public string Seats {get; set;}

    public ReservationModel()
    {
    }
    public ReservationModel(int id, string name, int ticketsCount, decimal price, string seats) //MovieModel movieModel
    {
        Id = id;
        Name = name;
        //ObjMovieModel = movieModel;
        TicketsCount = ticketsCount;
        Price = price;
        Seats = seats;
    }
}
