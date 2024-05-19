using System.Reflection;
using System.Text.Json.Serialization;
using System.Text;

class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id {get; set;}

    // [JsonPropertyName("movieObject")]
    // public MovieModel ObjMovieModel {get; set;}

    [JsonPropertyName("accountId")]
    public int AccountId {get; set;}

    [JsonPropertyName("sessionId")]
    public int SessionId {get; set;}

    [JsonPropertyName("movieId")]
    public int MovieId {get; set;}

    [JsonPropertyName("seats")]
    public string Seats {get; set;}

    [JsonPropertyName("food")]
    public string[] Food {get; set;}

    [JsonPropertyName("totalPrice")]
    public decimal TotalPrice {get; set;}

    [JsonPropertyName("purchaseTime")]
    public DateTime PurchaseTime {get; set;}


    public ReservationModel()
    {
    }

    public ReservationModel(int accountId, int sessionId, int movieId, string seats, string[] food, decimal totalPrice, DateTime purchaseTime) //MovieModel movieModel
    {
        AccountId = accountId;
        SessionId = sessionId;
        MovieId = movieId;
        // TicketsCount = ticketsCount;
        Seats = seats;
        Food = food; // An array because it shouldn't be updated after the reservation has been made anyway.
        TotalPrice = totalPrice;
        PurchaseTime = purchaseTime;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (PropertyInfo property in this.GetType().GetProperties())
        {
            var value = property.GetValue(this, null);
            sb.AppendLine($"{property.Name}: {value}");
        }
        return sb.ToString();
    }
}
