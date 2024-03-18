static class Adding
{
    static private FoodLogic foodLogic = new FoodLogic();
    static private MovieLogic movieLogic = new MovieLogic();


    public static void addFood()
    {
        Console.WriteLine("Add food to the menu:");
        Console.WriteLine("Please enter the name of the food item");
        string name = Console.ReadLine();
        Console.WriteLine("Please enter the price of the food item");
        decimal price = Convert.ToDecimal(Console.ReadLine());
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
        Console.WriteLine("Please enter the description of the movie");
        string description = Console.ReadLine();
        Console.WriteLine("Please enter the director of the movie");
        string director = Console.ReadLine();
        Console.WriteLine("Please enter the duration of the movie (minutes)");
        int duration = Convert.ToInt32(Console.ReadLine());
        MovieModel movies = new MovieModel(name, genre, description, director, duration);
        movieLogic.UpdateList(movies);

    }
}