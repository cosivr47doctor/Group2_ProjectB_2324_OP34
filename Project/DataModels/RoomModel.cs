using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using System.Collections;
using System.Text;


public class RoomModel : IModel
{
    [JsonPropertyName("Room")]
    public int Id { get; set; }

    [JsonPropertyName("Seats")]
    public List<List<string>> Seats {get; set;}

    [JsonPropertyName("Price standard seat")]
    public decimal Price1 { get; set; }

    [JsonPropertyName("Price deluxe seat")]
    public decimal Price2 { get; set; }

    [JsonPropertyName("Price vip seat")]
    public decimal Price3 { get; set; }
    
    public RoomModel(List<List<string>> seats, decimal price1, decimal price2, decimal price3)
    {
        Id = 0;
        Seats = seats;
        Price1 = price1;
        Price2 = price2;
        Price3 = price3;
    }
}
