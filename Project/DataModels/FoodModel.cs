using System.Text.Json.Serialization;


class FoodModel
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Price")]
    public decimal Price { get; set; }

    public FoodModel(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

}
