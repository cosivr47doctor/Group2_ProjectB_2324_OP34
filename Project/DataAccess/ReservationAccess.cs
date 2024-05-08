using System.Runtime.CompilerServices;
using System.Text.Json;

static class ReservationAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));


    public static List<ReservationModel2> LoadAll()
    {
        string json = File.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<ReservationModel2>();
        }
        return JsonSerializer.Deserialize<List<ReservationModel2>>(json);
    }


    public static void WriteAll(List<ReservationModel2> reservationItem)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(reservationItem, options);
        File.WriteAllText(path, json);
    }

}