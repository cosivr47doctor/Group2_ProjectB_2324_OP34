using System.Text.Json.Serialization;
using System.Text;

class MovieScheduleModel : IModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("room")]
    public int Room { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("sessionTime")]
    public Dictionary<string, string> TimeIdPair { get; set; }

    [JsonPropertyName("movieId")]
    public int MovieId {get; set;}

    // Exclude from JSON serialization
    // Data gets erased after the first serialization & deserialization, so no data is accessed in the overriden ToString() method
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
            TimeIdPair[kvp.Key] = $"Duration: {kvp.Value.Duration} minutes";
            MovieId = kvp.Value.Id;
        }

    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"ID: {Id}");
        sb.AppendLine($"Room: {Room}");
        sb.AppendLine($"Date: {Date.ToString("yyyy-MM-dd")}");
        foreach (var kvp in TimeIdPair)
        {
            string timeslot = kvp.Key.ToString(); // Convert TimeSpan to string
            sb.AppendLine($"Timeslot: {timeslot}");
        }
        int idOfMovie = this.MovieId;
        var movie = GenericAccess<MovieModel>.LoadAll().FirstOrDefault(m => m.Id == idOfMovie);
        string movieTitle = movie?.Name ?? GenericAccess<MovieModel>.LoadAll().Last().Name;
        sb.AppendLine($"Movie title: {movieTitle}");
        return sb.ToString();
    }

}
