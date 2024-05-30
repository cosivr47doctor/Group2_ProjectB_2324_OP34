// Movie details needed for the MovieScheduleModel
using System.Text.Json.Serialization;


public class MovieDetailsModel : IModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    public string Title {get; set;}
    public int Duration {get; set;}

    public MovieDetailsModel() { } // Parameterless constructor
    public MovieDetailsModel(int id, string title, int duration)
    {
        Id = id;
        Title = title;
        Duration = duration;
    }

}
