using System.Text.Json.Serialization;
using System.Text;
using System.Collections.Generic;

class MovieScheduleModel : ICloneable
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("room")]
    public int Room { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("Time")]
    public Dictionary<string, string> TimeIdPair { get; set; }

    // Exclude from JSON serialization
    [JsonIgnore]
    public Dictionary<string, string> TimeTitlePair {get; private set;}
    [JsonIgnore]
    public Dictionary<string, MovieDetailsModel> TimeDict {get; set;} = new Dictionary<string, MovieDetailsModel>();

    // Parameterless constructor for serialization
    public MovieScheduleModel() { }

    // Deserialization constructor
    [JsonConstructor]
    public MovieScheduleModel(int id, int room, DateTime date, Dictionary<string, MovieDetailsModel> timeDict)
    {
        Id = id;
        Room = room;
        Date = date;
        TimeDict = timeDict ?? new Dictionary<string, MovieDetailsModel>();

        // Initialise Time dictionary
        secConstructor();
    }

    public void secConstructor()
    {
        TimeIdPair = new Dictionary<string, string>() {};
        foreach (var kvp in TimeDict)
        {
            TimeIdPair[kvp.Key] = $"MovieId: {kvp.Value.Id}";
        }

        TimeTitlePair = new Dictionary<string, string>() {};
        foreach (var kvp in TimeDict)
        {
            TimeTitlePair[kvp.Key] = kvp.Value.Title;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"ID: {Id}");
        sb.AppendLine($"Room: {Room}");
        sb.AppendLine($"Date: {Date.ToString("yyyy-MM-dd")}");
        string tempsbLine = "";
        foreach (var kvp in TimeIdPair)
        {
            string timeslot = kvp.Key.ToString(); // Convert TimeSpan to string
            tempsbLine += $"Timeslot: {timeslot}, ";
        }
        foreach (KeyValuePair<string, string> kvp in TimeTitlePair)
        {
            tempsbLine += $"Movie: {kvp.Value}";
        }
        sb.AppendLine(tempsbLine);
        return sb.ToString();
    }

    public object Clone()  // Unused
    {
        return null;
    }
}
