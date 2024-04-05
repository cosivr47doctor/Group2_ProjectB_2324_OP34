using System.Text.Json.Serialization;


class AccountModel
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

    [JsonPropertyName("suspense")]
    public DateTime? Suspense { get; set; } = null;

    [JsonPropertyName("PhoneNumber")]
    public int PhoneNumber { get; set; }

    [JsonPropertyName("reservations")]
    public List<ReservationModel> Reservations {get; set;} = new List<ReservationModel>();
    // ^^^ The desired format for the string:    `Id: {?}, Title: {?}`




    public AccountModel(int id, string emailAddress, string password, string fullName, int phoneNumber)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

}




