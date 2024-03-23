using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class MovieLogic
{
    private List<MovieModel> _movie;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself

    //static public FoodModel? CurrentFoodItem { get; private set; }

    public MovieLogic()
    {
        _movie = MovieAccess.LoadAll();
    }


    public void UpdateList(MovieModel movies)
    {
        //Find if there is already an model with the same id
        int maxId = _movie.Count > 0 ? _movie.Max(m => m.Id) : 0;
        int index = _movie.FindIndex(s => s.Name == movies.Name);

        if (index != -1)
        {
            //update existing model
            _movie[index] = movies;
        }
        else
        {
            //add new model
            movies.Id = maxId + 1;
            _movie.Add(movies);
        }
        MovieAccess.WriteAll(_movie);

    }

    public MovieModel GetBySearch(string searchBy)
    {
        //Only gets one result for now
        string searchLower = searchBy.ToLower();

        return _movie.Find(movie =>
            movie.Id.ToString() == searchBy ||
            movie.Year.ToString() == searchBy ||
            movie.Name.ToLower().Contains(searchLower) ||
            movie.Director.ToLower().Contains(searchLower) ||
            movie.Genre.ToLower().Contains(searchLower));
    }

    public void RemoveMovie(string searchBy)
    {
        MovieModel movieToRemove = GetBySearch(searchBy);

        if (movieToRemove != null)
        {
            _movie.Remove(movieToRemove);
            MovieAccess.WriteAll(_movie);
        }
    }



    // public MovieModel GetById(string searchBy)
    // {
    //     if (int.TryParse(searchBy, out int id))
    //     {
    //         return _movie.Find(movie => movie.Id == id);
    //     }
    //     else
    //     {
    //         return _movie.Find(movie => movie.Name.ToLower() == searchBy.ToLower());
    //     }
        
    // }

    // public AccountModel CheckLogin(string email, string password)
    // {
    //     if (email == null || password == null)
    //     {
    //         return null;
    //     }
    //     CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
    //     return CurrentAccount;
    // }
}
