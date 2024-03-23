static class Adding
{
    static private FoodLogic foodLogic = new FoodLogic();
    static private MovieLogic movieLogic = new MovieLogic();


    public static void addFood()
    {
        //need id
        Console.WriteLine("Add food to the menu:");
        Console.WriteLine("Please enter the name of the food item");
        string name = Console.ReadLine();

        decimal price;
        while (true)
        {
            Console.WriteLine("Please enter the price of the food item");
            string priceInput = Console.ReadLine();
            if (decimal.TryParse(priceInput, out price))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid price.");
            }
        }
        
        FoodModel foodItem = new FoodModel(name, price);
        foodLogic.UpdateList(foodItem);

    }

    public static void addMovie()
    {
        Console.WriteLine("Add movie to the cinema:");
        Console.WriteLine("Please enter the name of the movie");
        string name = Console.ReadLine();
        Console.WriteLine("Please enter the genre of the movie");
        string genre = Console.ReadLine();

        int year;
        while (true)
        {
            Console.WriteLine("Please enter the year of the movie");
            string yearInput = Console.ReadLine();
            if (int.TryParse(yearInput, out year))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid year.");
            }
        }

        Console.WriteLine("Please enter the description of the movie");
        string description = Console.ReadLine();
        Console.WriteLine("Please enter the director of the movie");
        string director = Console.ReadLine();

        int duration;
        while (true)
        {
            Console.WriteLine("Please enter the duration of the movie (minutes)");
            string durationInput = Console.ReadLine();
            if (int.TryParse(durationInput, out duration))
            {
                break; 
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid duration.");
            }
        }

        MovieModel movies = new MovieModel(name, genre, year, description, director, duration);
        movieLogic.UpdateList(movies);

    }
}