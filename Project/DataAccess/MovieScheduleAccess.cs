using System.Text.Json;
static class MovieSchedulingAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/movieSessions.json"));

    public static List<MovieScheduleModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<MovieScheduleModel>();
        }
        return JsonSerializer.Deserialize<List<MovieScheduleModel>>(json);
    }

    public static void WriteAll(List<MovieScheduleModel> movieSchedules)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(movieSchedules, options);
        File.WriteAllText(path, json);
    }
}
