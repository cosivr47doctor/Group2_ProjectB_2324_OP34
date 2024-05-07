// Movie details needed for the MovieScheduleModel
using System.Text.Json.Serialization;


class MovieDetailsModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    public MovieDetailsModel(int id)
    {
        Id = id;
    }
}
