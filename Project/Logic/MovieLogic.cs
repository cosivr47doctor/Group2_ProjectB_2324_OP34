using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Reflection;
using System.Linq;


//This class is not static so later on we can use inheritance and interfaces
public class MovieLogic : AbstractLogic<MovieModel>
{
    private List<MovieModel> _movie;
    public List<MovieModel> Movies => _movie;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself

    //static public FoodModel? CurrentFoodItem { get; private set; }

    public MovieLogic()
    {
        if (TestEnvironmentUtils.IsRunningInUnitTest()) _movie = MovieAccessForJson.LoadAllJson();
        else _movie = GenericAccess<MovieModel>.LoadAll();
    }

    public MovieModel GetBySearch(int id) => _movie.Find(movie => movie.Id == id);
    public MovieModel GetBySearch(string searchBy)
    {
        string searchLower = searchBy.ToLower();
        if (ConsoleE.IsNullOrEmptyOrWhiteSpace(searchLower)) return null;
        
        return _movie.Find(movie =>
            movie.Id.ToString() == searchBy ||
            movie.Year.ToString() == searchBy ||
            movie.Name.ToLower().StartsWith(searchLower) ||  // This is unhandy in case of movies with duplicate names
            movie.Director.ToLower().StartsWith(searchLower));  // This is unhandy in case of movies with duplicate directors
    }

    public override List<MovieModel> GetAllBySearch(string searchBy) // finds all for searching options
    {
        string searchLower = searchBy.ToLower();

        return _movie.Where(movie =>
            movie.Id.ToString() == searchBy ||
            movie.Year.ToString() == searchBy ||
            movie.Name.ToLower().Contains(searchLower) ||  
            movie.Director.ToLower() == searchLower).ToList();  
    }

    public MovieModel SelectForResv(string searchBy)
    {
        string searchLower = searchBy.ToLower();

        return _movie.Find(movie =>
            movie.Id.ToString() == searchBy ||
            movie.Name.ToLower().Contains(searchLower));  // This is unhandy in case of movies with duplicate directors
    }

    public void ChangeMovie(string input)
    {
        Console.ResetColor();
        string searchBy;
        string[] unitInput = null;
        if (input.Contains(';'))
        {
            unitInput = input.Split(';');
        }

        if (TestEnvironmentUtils.IsRunningInUnitTest()) searchBy = unitInput[0];
        else searchBy = input;
        MovieModel movieToChange = GetBySearch(searchBy);
        if (movieToChange == null)
        {
            Console.WriteLine("Movie not found.");
            return;
        }
        int index = Movies.FindIndex(m => m.Id == movieToChange.Id);

        Console.WriteLine("Invalid inputs will simply be ingored\n");

        Console.WriteLine("Please enter the new title (blank if unchanged)");
        string changeNameInput = TestEnvironmentUtils.ReadLine();
        // string changeNameInput = TestEnvironmentUtils.ReadLine();
        Console.WriteLine("Please enter the new genre (blank if unchanged)");
        string changeGenreInput = TestEnvironmentUtils.ReadLine();
        Console.WriteLine("Please enter the new year of release (blank if unchanged)");
        string changeYearInput = TestEnvironmentUtils.ReadLine();
        Console.WriteLine("Please enter the new description (blank if unchanged)");
        string changeDescriptionInput = TestEnvironmentUtils.ReadLine();
        Console.WriteLine("Please enter the new director (blank if unchanged)");
        string changeDirectorInput = TestEnvironmentUtils.ReadLine();
        Console.WriteLine("Please enter the new duration (blank if unchanged)");
        string changeDurationInput = TestEnvironmentUtils.ReadLine();

        if (TestEnvironmentUtils.IsRunningInUnitTest())
        {
            if (unitInput.Length > 1 && !string.IsNullOrEmpty(unitInput[1])) movieToChange.Name = unitInput[1];
            if (unitInput.Length > 2 && !string.IsNullOrEmpty(unitInput[2])) movieToChange.Genre = unitInput[2].Split(",");
            if (unitInput.Length > 3 && !string.IsNullOrEmpty(unitInput[3]) && int.TryParse(unitInput[3], out _)) movieToChange.Year = Convert.ToInt16(unitInput[3]);
            if (unitInput.Length > 4 && !string.IsNullOrEmpty(unitInput[4])) movieToChange.Description = unitInput[4];
            if (unitInput.Length > 5 && !string.IsNullOrEmpty(unitInput[5])) movieToChange.Director = unitInput[5];
            if (unitInput.Length > 6 && !string.IsNullOrEmpty(unitInput[6]) && int.TryParse(unitInput[6], out _)) movieToChange.Duration = Convert.ToInt16(unitInput[6]);            
        }
        else
        {
            if (!string.IsNullOrEmpty(changeNameInput)) movieToChange.Name = changeNameInput;
            if (!string.IsNullOrEmpty(changeGenreInput)) movieToChange.Genre = changeGenreInput.Split(",");
            if (!string.IsNullOrEmpty(changeYearInput) && int.TryParse(changeYearInput, out _)) movieToChange.Year = Convert.ToInt16(changeYearInput);
            if (!string.IsNullOrEmpty(changeDescriptionInput)) movieToChange.Description = changeDescriptionInput;
            if (!string.IsNullOrEmpty(changeDirectorInput)) movieToChange.Director = changeDirectorInput;
            if (!string.IsNullOrEmpty(changeDurationInput) && int.TryParse(changeDurationInput, out _)) movieToChange.Duration = Convert.ToInt16(changeDurationInput);
        }


        _movie[index] = movieToChange;
        if (TestEnvironmentUtils.IsRunningInUnitTest()) MovieAccessForJson.WriteAllJson(Movies);
        else GenericAccess<MovieModel>.WriteAll(_movie);
        Console.WriteLine(movieToChange);
    }

    public void RemoveMovie(string searchBy)
    {
        Console.ResetColor();
        MovieModel movieToRemove = GetBySearch(searchBy);

        if (movieToRemove != null)
        {
            _movie.Remove(movieToRemove);
            GenericAccess<MovieModel>.WriteAll(_movie);
        }
    }

    public void CloneMovie(string searchBy)
    {
        Console.ResetColor();
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

        if (!ConsoleE.IsNullOrEmptyOrWhiteSpace(movieToClone))
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
            GenericMethods.UpdateList(clonedMovie);
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

    public static MovieModel SelectRandomMovie()
    {
        List<MovieModel> allMovies = GenericAccess<MovieModel>.LoadAll();
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
