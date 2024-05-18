using System.Collections;
using System.Collections.Generic;

public class MovieCollection : IEnumerable<MovieModel>
{
    private List<MovieModel> movies;

    public MovieCollection()
    {
        movies = new List<MovieModel>();
    }

    public void Add(MovieModel movie)
    {
        movies.Add(movie);
    }

    public IEnumerator<MovieModel> GetEnumerator() => movies.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
