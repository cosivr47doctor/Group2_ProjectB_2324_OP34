using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class MovieLogic
{
    private List<MovieModel> _movie;
    public List<MovieModel> Movies => _movie;

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
        string searchLower = searchBy.ToLower();

        return _movie.Find(movie =>
            movie.Id.ToString() == searchBy ||
            movie.Year.ToString() == searchBy ||
            movie.Name.ToLower().Contains(searchLower) ||  // This is unhandy in case of movies with duplicate names
            movie.Director.ToLower().Contains(searchLower));  // This is unhandy in case of movies with duplicate directors
    }

    public MovieModel SelectForResv(string searchBy)
    {
        string searchLower = searchBy.ToLower();

        return _movie.Find(movie =>
            movie.Id.ToString() == searchBy ||
            movie.Name.ToLower().Contains(searchLower));  // This is unhandy in case of movies with duplicate directors
    }

    public void ChangeMovie(string searchBy)
    {
        MovieModel movieToChange = GetBySearch(searchBy);
        int index = Movies.FindIndex(m => m.Id == movieToChange.Id);

        if (movieToChange != null)
        {
            Console.WriteLine("Invalid inputs will simply be ingored\n");

            Console.WriteLine("Please enter the new title (blank if unchanged)");
            string changeNameInput = Console.ReadLine();
            Console.WriteLine("Please enter the new genre (blank if unchanged)");
            string changeGenreInput = Console.ReadLine();
            Console.WriteLine("Please enter the new year of release (blank if unchanged)");
            string changeYearInput = Console.ReadLine();
            Console.WriteLine("Please enter the new description (blank if unchanged)");
            string changeDescriptionInput = Console.ReadLine();
            Console.WriteLine("Please enter the new director (blank if unchanged)");
            string changeDirectorInput = Console.ReadLine();
            Console.WriteLine("Please enter the new duration (blank if unchanged)");
            string changeDurationInput = Console.ReadLine();

            if (changeNameInput != null) movieToChange.Name = changeNameInput;
            if (changeGenreInput != null) movieToChange.Genre = changeNameInput.Split(",");
            if (changeYearInput != null && int.TryParse(changeYearInput, out _)) movieToChange.Year = Convert.ToInt16(changeYearInput);
            if (changeDescriptionInput != null) movieToChange.Description = changeDescriptionInput;
            if (changeDirectorInput != null) movieToChange.Director = changeDirectorInput;
            if (changeDurationInput != null && int.TryParse(changeDurationInput, out _)) movieToChange.Duration = Convert.ToInt16(changeDurationInput);


            _movie[index] = movieToChange;
            MovieAccess.WriteAll(_movie);
        }
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

    public MovieModel SelectRandomMovie()
    {
        List<MovieModel> allMovies = MovieAccess.LoadAll();
        if (allMovies.Count == 0)
        {
            return null; // No movies loaded
        }

        Random random = new Random();
        int randomIndex = random.Next(0, allMovies.Count);

        return allMovies[randomIndex];
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
