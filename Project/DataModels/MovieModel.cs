using System.Text.Json.Serialization;
using Microsoft.VisualBasic;


class MovieModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Genre")]
    public string Genre { get; set; }

    [JsonPropertyName("Year")]
    public int Year { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("Director")]
    public string Director { get; set; }

    [JsonPropertyName("Duration")]
    public int Duration { get; set; }


    public MovieModel(string name, string genre, int year, string description, string director, int duration)
    {
        Id = 0;
        Name = name;
        Genre = genre;
        Year = year;
        Description = description;
        Director = director;
        Duration = duration;
    }

}