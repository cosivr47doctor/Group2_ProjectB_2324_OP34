/*
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

// Generic Access class for AccountModel, FoodModel, MovieModel, MovieScheduleModel, and ReservationModel
public class GenericAccessJson<TModel>
{
    public IFileWrapper FileWrapper {get; set;} = new FileWrapper();
    static string BaseDirectory { get; set; } = Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"Project"));
    public string CustomPath { get; set; } = Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));

    public List<MovieModel> unitLoadMM()
    {
        return new List<MovieModel>();
    }
    public List<TModel> LoadAllJson()
    {
        PathFinder();
        string path = CustomPath;
        string json = FileWrapper.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<TModel>();
        }
        return JsonSerializer.Deserialize<List<TModel>>(json);
    }

    public void WriteAllJson(List<MovieModel> movies)
    {
        PathFinder();
        string path = CustomPath;
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(movies, options);
        FileWrapper.WriteAllText(path, json);
    }

    private void PathFinder()
    {
        string path = "";
        string modelName = typeof(TModel).Name.ToLower();
        if (modelName == "accountmodel") path = @"DataSources/accounts.json";
        else if (modelName == "foodmodel") path = @"DataSources/food.json";
        else if (modelName == "moviemodel") path = @"DataSources/movies.json";
        else if (modelName == "movieschedulemodel") path = @"DataSources/movieSessions.json";
        else if (modelName == "reservationmodel") path = @"DataSources/reservations.json";
        else if (modelName == "roommodel") path = @"DataSources/rooms.json";

        CustomPath = path;
    }
}
*/
