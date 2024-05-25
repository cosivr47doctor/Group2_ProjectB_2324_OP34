using System.Text.Json;
using System.Collections.Generic;
using System.IO;

// Generic Access class for AccountModel, FoodModel, MovieModel, MovieScheduleModel, and ReservationModel
public static class GenericAccess<TModel>
{
    public static IFileWrapper FileWrapper = new FileWrapper();
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
    public static List<MovieModel> LoadAllJson()
    {
        string path = PathFinder();
        string json = FileWrapper.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<MovieModel>();
        }
        return JsonSerializer.Deserialize<List<MovieModel>>(json);
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


    /*private static string PathFinder()
    {
        string path = "";
        string modelName = typeof(TModel).Name.ToLower();
        if (modelName is AccountModel) path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
        else if (modelName is FoodModel) path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/food.json"));
        else if (modelName is MovieModel) path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/movies.json"));
        else if (modelName is MovieScheduleModel) path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/movieSessions.json"));
        else if (modelName is ReservationModel) path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));

        return path;
    }*/

    private static string PathFinder()
    {
        string path = "";
        string modelName = typeof(TModel).Name.ToLower();
        if (modelName == "accountmodel") path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
        else if (modelName == "foodmodel") path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/food.json"));
        else if (modelName == "moviemodel") path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/movies.json"));
        else if (modelName == "movieschedulemodel") path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/movieSessions.json"));
        else if (modelName == "reservationmodel") path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));
        else if (modelName == "roommodel") path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/rooms.json"));

        return path;
    }
}

