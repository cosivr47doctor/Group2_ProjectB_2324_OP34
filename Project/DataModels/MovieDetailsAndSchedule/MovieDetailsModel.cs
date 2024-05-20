// Movie details needed for the MovieScheduleModel
using System.Text.Json.Serialization;


class MovieDetailsModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    public string Title {get; set;}

    public MovieDetailsModel() { } // Parameterless constructor
    public MovieDetailsModel(int id, string title)
    {
        Id = id;
        Title = title;
    }

}
