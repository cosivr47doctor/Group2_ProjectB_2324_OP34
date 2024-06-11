using System.Text.Json.Serialization;

public class LocalIpModel : IModel
{
    [JsonPropertyName("Id")]
    public int Id {get; set;}

    [JsonPropertyName("LocalIPAddress")]
    public string LocalIP {get; set;} = null;

    [JsonPropertyName("AccId")]
    public static int accId {get; set;}

    public LocalIpModel(string ip)
    {
        Id = GenericAccess<LocalIpModel>.LoadAll().Last().Id + 1;
        LocalIP = ip;
    }
}
