using System.Runtime.CompilerServices;
using System.Text.Json;

static class ReservationAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));


    public static List<ReservationModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<ReservationModel>();
        }
        return JsonSerializer.Deserialize<List<ReservationModel>>(json);
    }


    public static void WriteAll(List<ReservationModel> reservationItem)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(reservationItem, options);
        File.WriteAllText(path, json);
    }

}