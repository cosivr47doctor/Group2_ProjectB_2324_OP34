using System.Runtime.CompilerServices;
using System.Text.Json;

public static class MovieAccess
{
    public static IFileWrapper FileWrapper = new FileWrapper();
    static string path = Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/movies.json"));


    public static List<MovieModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<MovieModel>();
        }
        List<MovieModel> movieList = JsonSerializer.Deserialize<List<MovieModel>>(json);
        MovieCollection movieCollection = new MovieCollection();
        foreach (var movie in movieList)
        {
            movieCollection.Add(movie);
        }
        return movieCollection.ToList();
    }

    public static List<MovieModel> LoadAllJson()
    {
        string json = FileWrapper.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<MovieModel>();
        }
        List<MovieModel> movieList = JsonSerializer.Deserialize<List<MovieModel>>(json);
        MovieCollection movieCollection = new MovieCollection();
        foreach (var movie in movieList)
        {
            movieCollection.Add(movie);
        }
        return movieCollection.ToList() ?? new List<MovieModel>( );
    }


    public static void WriteAllJson(List<MovieModel> movies)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(movies, options);
        FileWrapper.WriteAllText(path, json);
    }

    public static void WriteAll(List<MovieModel> movies)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(movies, options);
        File.WriteAllText(path, json);
    }

}
