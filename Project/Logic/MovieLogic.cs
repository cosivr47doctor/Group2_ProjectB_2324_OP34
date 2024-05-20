using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Reflection;


//This class is not static so later on we can use inheritance and interfaces
public class MovieLogic
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


    public void UpdateList(MovieModel movie)
    {
        //Find if there is already an model with the same id
        int maxId = _movie.Count > 0 ? _movie.Max(m => m.Id) : 0;
        int index = _movie.FindIndex(s => s.Name == movie.Name);

        if (index != -1)
        {
            //update existing model
            _movie[index] = movie;
        }
        else
        {
            //add new model
            movie.Id = maxId + 1;
            _movie.Add(movie);
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
        if (movieToChange == null)
        {
            Console.WriteLine("Movie not found.");
            return;
        }
        int index = Movies.FindIndex(m => m.Id == movieToChange.Id);

        Console.WriteLine("Invalid inputs will simply be ingored\n");

        Console.WriteLine("Please enter the new title (blank if unchanged)");
        string changeNameInput = ConsoleE.Input();
        Console.WriteLine("Please enter the new genre (blank if unchanged)");
        string changeGenreInput = ConsoleE.Input();
        Console.WriteLine("Please enter the new year of release (blank if unchanged)");
        string changeYearInput = ConsoleE.Input();
        Console.WriteLine("Please enter the new description (blank if unchanged)");
        string changeDescriptionInput = ConsoleE.Input();
        Console.WriteLine("Please enter the new director (blank if unchanged)");
        string changeDirectorInput = ConsoleE.Input();
        Console.WriteLine("Please enter the new duration (blank if unchanged)");
        string changeDurationInput = ConsoleE.Input();

        if (!string.IsNullOrEmpty(changeNameInput)) movieToChange.Name = changeNameInput;
        if (!string.IsNullOrEmpty(changeGenreInput)) movieToChange.Genre = changeGenreInput.Split(",");
        if (!string.IsNullOrEmpty(changeYearInput) && int.TryParse(changeYearInput, out _)) movieToChange.Year = Convert.ToInt16(changeYearInput);
        if (!string.IsNullOrEmpty(changeDescriptionInput)) movieToChange.Description = changeDescriptionInput;
        if (!string.IsNullOrEmpty(changeDirectorInput)) movieToChange.Director = changeDirectorInput;
        if (!string.IsNullOrEmpty(changeDurationInput) && int.TryParse(changeDurationInput, out _)) movieToChange.Duration = Convert.ToInt16(changeDurationInput);


        _movie[index] = movieToChange;
        MovieAccess.WriteAll(_movie);
    }

    public void ChangeMovie(string[] args)
    {
        List<MovieModel> movies = MovieAccess.LoadAll();
        MovieModel movieToChange = movies.FirstOrDefault(m => m.Id == int.Parse(args[0]));
        if (movieToChange == null)
        {
            Console.WriteLine("Movie not found.");
            return;
        }
        int index = Movies.FindIndex(m => m.Id == movieToChange.Id);

        if (!ConsoleE.IsNullOrEmptyOrWhiteSpace(args[1])) movieToChange.Name = args[1];
        if (!ConsoleE.IsNullOrEmptyOrWhiteSpace(args[2])) movieToChange.Genre = args[2].Split(",");
        if (!ConsoleE.IsNullOrEmptyOrWhiteSpace(args[3]) && int.TryParse(args[3], out _)) movieToChange.Year = Convert.ToInt16(args[3]);
        if (!ConsoleE.IsNullOrEmptyOrWhiteSpace(args[4])) movieToChange.Description = args[4];
        if (!ConsoleE.IsNullOrEmptyOrWhiteSpace(args[5])) movieToChange.Director = args[5];
        if (!ConsoleE.IsNullOrEmptyOrWhiteSpace(args[6]) && int.TryParse(args[6], out _)) movieToChange.Duration = Convert.ToInt16(args[6]);    

        movies[index] = movieToChange;
        MovieAccess.WriteAll(_movie);
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

    public void CloneMovie(string searchBy)
    {
        string[] details = null;
        if (TestEnvironmentUtils.IsRunningInUnitTest())
        {
            var inputs = searchBy.Split(" ");
            searchBy = inputs[0]; // First part is the searchBy input
            if (inputs.Length > 1)
            {
                details = inputs.Skip(1).ToArray(); // Remaining parts are details
            }
        }
        MovieModel movieToClone = GetBySearch(searchBy);

        if (movieToClone != null)
        {
            List<object> newPropertyValues  = new List<object>();
            PropertyInfo[] movieProperties = movieToClone.GetType().GetProperties();
            int detailIndex = 0;

            foreach (PropertyInfo property in movieProperties.Skip(1))
            {
                string userOrUnitInput = string.Empty;
                if (!TestEnvironmentUtils.IsRunningInUnitTest())
                {
                    userOrUnitInput = ConsoleE.Input($"Enter new {property.Name}. Blank if unchanged.");
                }
                else
                {
                    if (details != null && detailIndex < details.Length)
                    {
                        userOrUnitInput = details[detailIndex];
                        detailIndex++;
                    }
                }
                if (!String.IsNullOrEmpty(userOrUnitInput) && !String.IsNullOrWhiteSpace(userOrUnitInput))
                {
                    try
                    {
                        object newValue;
                        if (property.PropertyType == typeof(string[]))
                        {
                            newValue = userOrUnitInput.Split(',').Select(s => s.Trim()).ToArray();
                        }
                        else
                        {
                            newValue = Convert.ChangeType(userOrUnitInput, property.PropertyType);
                        }
                        newPropertyValues.Add(newValue);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid input. Did you use characters where digits are expected?");
                        return;
                    }
                }
                else newPropertyValues.Add(property.GetValue(movieToClone));
            }

            MovieModel clonedMovie = CreateMovieFromValues(newPropertyValues);
            clonedMovie.Id = 0;
            UpdateList(clonedMovie);
            Console.WriteLine("Movie succesfully cloned");
        }
        else
        {
            Console.WriteLine("Movie not found for cloning.");
        }
    }

    private MovieModel CreateMovieFromValues(List<object> propertyValues)
    {
        return new MovieModel((string)propertyValues[0], (string[])propertyValues[1], 
                            (int)propertyValues[2], (string)propertyValues[3], 
                            (string)propertyValues[4], (int)propertyValues[5]);
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
