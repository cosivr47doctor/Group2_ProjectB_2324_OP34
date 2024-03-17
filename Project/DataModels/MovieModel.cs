using System.Text.Json.Serialization;


class AccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Description")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("Director")]
    public string Password { get; set; }

    [JsonPropertyName("Duration")]
    public DateTime? Suspense { get; set; } = null;


    public AccountModel(int id, string emailAddress, string password, string fullName)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
    }

}