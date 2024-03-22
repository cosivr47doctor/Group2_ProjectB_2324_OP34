using Newtonsoft.Json;
using System;
public static class SeeJsons
{
    public static void PrintFoodJson(string filePath)
    {
        // split food and drinks
        List<FoodModel> existingFoodItems = FoodAccess.LoadAll();
        Console.WriteLine("Cinema food items:");
        Console.WriteLine("-----------------------------------");
        foreach (var foodItem in existingFoodItems)
        {
            Console.WriteLine(foodItem.Name);
            Console.WriteLine($"Price: {foodItem.Price}");
            Console.WriteLine("-----------------------------------");
        }
    }

    public static void PrintMoviesJson(string filePath)
    {
        List<MovieModel> existingMovies = MovieAccess.LoadAll();
        Console.WriteLine("Cinema movies:");
        Console.WriteLine("-----------------------------------");

        foreach (var movie in existingMovies)
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