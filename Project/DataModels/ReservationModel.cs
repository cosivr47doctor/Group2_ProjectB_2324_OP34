using System.Text.Json.Serialization;

class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id {get; set;}

    [JsonPropertyName("movieObject")]
    public MovieModel ObjMovieModel {get; set;}

    [JsonPropertyName("ticketsCount")]
    public int TicketsCount {get; set;}

    public ReservationModel()
    {
    }
    public ReservationModel(int id, MovieModel movieModel, int ticketsCount)
    {
        Id = id;
        ObjMovieModel = movieModel;
        TicketsCount = ticketsCount;
    }
}
