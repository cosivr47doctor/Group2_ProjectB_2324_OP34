using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using System.Collections;
using System.Text;


public class MovieModel : IModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Genre")]
    public string[] Genre { get; set; }

    [JsonPropertyName("Year")]
    public int Year { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("Director")]
    public string Director { get; set; }

    [JsonPropertyName("Duration")]
    public int Duration { get; set; }

    public MovieModel() {}
    public MovieModel(string name, string[] genre, int year, string description, string director, int duration)
    {
        Id = 0;
        Name = name;
        Genre = genre;
        Year = year;
        Description = description;
        Director = director;
        Duration = duration;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"ID: {Id}"); sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Genre: {String.Join(", ", Genre)}");
        sb.AppendLine($"Year: {Year}");
        sb.AppendLine($"Description: {Description}"); sb.AppendLine($"Director: {Director}"); sb.AppendLine($"Duration: {Duration}");
        return sb.ToString();
    }
}
