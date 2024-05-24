// Movie details needed for the MovieScheduleModel
using System.Text.Json.Serialization;


class MovieDetailsModel : ICloneable, IModel
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

    public object Clone() // unused
    {
        // Create a new instance of MovieDetailsModel
        MovieDetailsModel clonedMovieDetails = new MovieDetailsModel()
        {
            Id = this.Id,
            Title = this.Title,
            Duration = this.Duration,
            // Copy other properties as needed
        };

        return clonedMovieDetails;
    }

}
