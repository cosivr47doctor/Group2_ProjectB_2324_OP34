using System.Text.Json.Serialization;


public class AccountModel : IModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("isAdmin")]
    public bool isAdmin {get; set;}

/*
    [JsonPropertyName("suspense")]
    public DateTime? Suspense { get; set; } = null; // ? to make a tenary expression
*/

    [JsonPropertyName("PhoneNumber")]
    public int PhoneNumber { get; set;}



    public AccountModel(int id, string emailAddress, string password, string fullName, int phoneNumber)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

}




