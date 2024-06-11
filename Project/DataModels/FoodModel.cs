using System.Text.Json.Serialization;


public class FoodModel : IModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Price")]
    public decimal Price { get; set; }

    public FoodModel(string name, decimal price)
    {
        Id = 0;
        Name = name;
        Price = price;
    }

}
