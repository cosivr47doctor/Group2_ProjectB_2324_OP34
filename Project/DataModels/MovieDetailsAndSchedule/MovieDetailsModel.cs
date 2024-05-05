// Movie details needed for the MovieScheduleModel
using System.Text.Json.Serialization;


class MovieDetailsModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("emailAddress")]
    public List<string> EmailAddress { get; set; }

    public MovieDetailsModel(string name, List<string> emailAddress)
    {
        Name = name;
        EmailAddress = emailAddress;
    }
}
