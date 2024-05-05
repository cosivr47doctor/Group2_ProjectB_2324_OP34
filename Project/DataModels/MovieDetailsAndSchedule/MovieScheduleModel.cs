using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text;

class MovieScheduleModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("room")]
    public int Room { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("Time")]
    public Dictionary<string, List<MovieDetailsModel>> Time { get; set; }

    // Parameterless constructor for serialization
    public MovieScheduleModel() { }

    // Deserialization constructor
    [JsonConstructor]
    public MovieScheduleModel(int id, int room, DateTime date, Dictionary<string, List<MovieDetailsModel>> time)
    {
        Id = id;
        Room = room;
        Date = date;
        Time = time;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"ID: {Id}");
        sb.AppendLine($"Room: {Room}");
        sb.AppendLine($"Date: {Date.ToString("yyyy-MM-dd")}");
        foreach (var kvp in Time)
        {
            string timeslot = kvp.Key.ToString(); // Convert TimeSpan to string
            string movieDetails = string.Join(", ", kvp.Value.Select(movie => movie.Name)); // Extract movie names
            sb.AppendLine($"Timeslot: {timeslot}, Movies: {movieDetails}");
        }
        return sb.ToString();
    }

    /*
    // Helper method to convert TimeSpan keys to strings
    private Dictionary<string, List<MovieDetailsModel>> ConvertTimeToStringKeys(Dictionary<TimeSpan, List<MovieDetailsModel>> time)
    {
        var convertedTime = new Dictionary<string, List<MovieDetailsModel>>();

        foreach (var kvp in time)
        {
            // Convert TimeSpan to string using total number of ticks
            string key = kvp.Key.Ticks.ToString();
            convertedTime.Add(key, kvp.Value);
        }

        return convertedTime;
    }
    */
}
