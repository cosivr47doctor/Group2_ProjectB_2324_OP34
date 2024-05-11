// Movie details needed for the MovieScheduleModel
using System.Text.Json.Serialization;


class MovieDetailsModel : ICloneable
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

    public object Clone() // unused
    {
        // Create a new instance of MovieDetailsModel
        MovieDetailsModel clonedMovieDetails = new MovieDetailsModel()
        {
            Id = this.Id,
            Title = this.Title,
            // Copy other properties as needed
        };

        return clonedMovieDetails;
    }

}
