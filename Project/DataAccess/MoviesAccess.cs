using System.Runtime.CompilerServices;
using System.Text.Json;

static class MovieAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/movies.json"));


    public static List<MovieModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<MovieModel>();
        }
        return JsonSerializer.Deserialize<List<MovieModel>>(json);
    }


    public static void WriteAll(List<MovieModel> movies)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(movies, options);
        File.WriteAllText(path, json);
    }

    public static MovieModel SelectRandomMovie()
    {
        List<MovieModel> allMovies = LoadAll();
        if (allMovies.Count == 0)
        {
            return null; // No movies loaded
        }

        Random random = new Random();
        int randomIndex = random.Next(0, allMovies.Count);

        return allMovies[randomIndex];
    }

}