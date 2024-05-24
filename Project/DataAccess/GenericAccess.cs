using System.Text.Json;

// Generic Access class for AccountModel, FoodModel, MovieModel, MovieScheduleModel, and ReservationModel
static class GenericAccess<TModel>
{
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


    public static void WriteAll(List<TModel> dataModelList)
    {
        string path = PathFinder();
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(dataModelList, options);
        File.WriteAllText(path, json);
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

