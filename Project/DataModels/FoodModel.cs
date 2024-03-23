using System.Text.Json.Serialization;


class FoodModel
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
