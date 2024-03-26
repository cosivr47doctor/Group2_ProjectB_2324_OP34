using Newtonsoft.Json;
using System;
public static class SeeJsons
{
    static private FoodLogic foodLogic = new FoodLogic();
    static private MovieLogic movieLogic = new MovieLogic();
    public static void PrintFoodJson(string filePath)
    {
        // split food and drinks
        
        // movieLogic.
        // List<FoodModel> existingFoodItems = FoodAccess.LoadAll();
        Console.WriteLine("Cinema food items:");
        Console.WriteLine("-----------------------------------");
        foreach (var foodItem in  foodLogic.foodItems)
        {
            Console.WriteLine(foodItem.Id);
            Console.WriteLine(foodItem.Name);
            Console.WriteLine($"Price: {foodItem.Price}");
            Console.WriteLine("-----------------------------------");
        }
    }

    public static void PrintMoviesJson(string filePath)
    {
        Console.WriteLine("Cinema movies:");
        Console.WriteLine("-----------------------------------");

        foreach (var movie in movieLogic.Movies)
        {
            Console.WriteLine($"Id: {movie.Id}");
            Console.WriteLine($"Name: {movie.Name}");
            Console.WriteLine($"Genre: {movie.Genre}");
            Console.WriteLine($"Year: {movie.Year}");
            Console.WriteLine($"Description: {movie.Description}");
            Console.WriteLine($"Director: {movie.Director}");
            Console.WriteLine($"Duration (in minutes): {movie.Duration}");
            Console.WriteLine("-----------------------------------");
        }
    }
    
    //FillJsons.PrintFoodJson(@"DataSources/food.json");
}