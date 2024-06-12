using System.Text.Json.Serialization;
using System.Text;


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

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Id: " + Id); sb.AppendLine("Name: " + Name); sb.AppendLine("Price: " + Price);
        return sb.ToString();
    }

}
