using System.Text.Json.Serialization;


class MovieModel
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Genre")]
    public string Genre { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("Director")]
    public string Director { get; set; }

    [JsonPropertyName("Duration")]
    public int Duration { get; set; }


    public MovieModel(string name, string genre, string description, string director, int duration)
    {
        Name = name;
        Genre = genre;
        Description = description;
        Director = director;
        Duration = duration;
    }

}