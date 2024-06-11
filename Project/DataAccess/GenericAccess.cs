using System.Text.Json;
using System.Collections.Generic;
using System.IO;

// Generic Access class for AccountModel, FoodModel, MovieModel, MovieScheduleModel, and ReservationModel
public static class GenericAccess<TModel>
{
    public static IFileWrapper FileWrapper {get; set;} = new FileWrapper();
    public static string BaseDirectory { get; set; } = Environment.CurrentDirectory;
    public static List<TModel> LoadAll()
    {
        string path = PathFinder();
        string json = File.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<TModel>();
        }
        return JsonSerializer.Deserialize<List<TModel>>(json);
    }
    public static List<MovieModel> unitLoadMM()
    {
        return new List<MovieModel>();
    }
    public static List<TModel> LoadAllJson()
    {
        string path = PathFinder();
        string json = FileWrapper.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<TModel>();
        }
        return JsonSerializer.Deserialize<List<TModel>>(json);
    }


    public static void WriteAll(List<TModel> dataModelList)
    {
        string path = PathFinder();
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(dataModelList, options);
        File.WriteAllText(path, json);
    }
    public static void WriteAllJson(List<MovieModel> movies)
    {
        string path = PathFinder();
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(movies, options);
        FileWrapper.WriteAllText(path, json);
    }

    private static string PathFinder()
    {
        string path = "";
        string modelName = typeof(TModel).Name.ToLower();
        if (modelName == "accountmodel") path = Path.GetFullPath(Path.Combine(BaseDirectory, @"DataSources/accounts.json"));
        else if (modelName == "foodmodel") path = Path.GetFullPath(Path.Combine(BaseDirectory, @"DataSources/food.json"));
        else if (modelName == "moviemodel") path = Path.GetFullPath(Path.Combine(BaseDirectory, @"DataSources/movies.json"));
        else if (modelName == "movieschedulemodel") path = Path.GetFullPath(Path.Combine(BaseDirectory, @"DataSources/movieSessions.json"));
        else if (modelName == "reservationmodel") path = Path.GetFullPath(Path.Combine(BaseDirectory, @"DataSources/reservations.json"));
        else if (modelName == "roommodel") path = Path.GetFullPath(Path.Combine(BaseDirectory, @"DataSources/rooms.json"));
        else if (modelName == "takenseatsmodel") path = Path.GetFullPath(Path.Combine(BaseDirectory, @"DataSources/takenSeats.json"));
        else if (modelName == "localipmodel") path = Path.GetFullPath(Path.Combine(BaseDirectory, @"DataSources/localips.json"));

        return Path.GetFullPath(path);
    }
}

