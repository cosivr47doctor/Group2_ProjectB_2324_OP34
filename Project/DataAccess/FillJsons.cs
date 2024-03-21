using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class FillJsons
{
    public static void FillAccountsJson(string filePath)
    {
        List<AccountModel> existingAccountItems = AccountsAccess.LoadAll();
    
        List<AccountModel> standardItems = new List<AccountModel>
        {
            new AccountModel(1, "n@b.c", "xyz", "Nikola Tesla")
        };
    
        foreach (var item in standardItems)
        {
            int index = existingAccountItems.FindIndex(account => account.EmailAddress == account.EmailAddress);

            if (index == -1)
            {
                existingAccountItems.Insert(0, item);
            }
        }
        AccountsAccess.WriteAll(existingAccountItems);
    }

    public static void FillFoodJson(string filePath)
    {
        List<FoodModel> existingFoodItems = FoodAccess.LoadAll();

        List<FoodModel> standardItems = new List<FoodModel>
        {
            new FoodModel("popcorn", 2.99m),
            new FoodModel("chips", 3.50m),
            new FoodModel("cola", 2.50m)
        };

        foreach (var item in standardItems)
        {
            int index = existingFoodItems.FindIndex(food => food.Name == item.Name);

            if (index == -1)
            {
                existingFoodItems.Insert(0, item);
            }
        }
        FoodAccess.WriteAll(existingFoodItems);
    }

    public static void FillMoviesJson(string filePath)
    {
        List<MovieModel> existingMovies = MovieAccess.LoadAll();

        List<MovieModel> standardMovies = new List<MovieModel>
        {
            new MovieModel("Inception", "Science Fiction", 210, "A thief who enters the dreams of others to steal their secrets from their subconscious.", "Christopher Nolan", 148),
            new MovieModel("The Shawshank Redemption", "Drama", 1994, "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", "Frank Darabont", 142),
            new MovieModel("The Godfather", "Crime", 1972, "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", "Francis Ford Coppola", 175),
            new MovieModel("The Matrix", "Action", 1999, "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", "Lana Wachowski, Lilly Wachowski", 136)
        };

        foreach (var item in standardMovies)
        {
            int index = existingMovies.FindIndex(movie => movie.Name == item.Name);

            if (index == -1)
            {
                existingMovies.Insert(0, item);
            }
        }
        MovieAccess.WriteAll(existingMovies);
    }

    // public static void PrintFoodJson(string filePath)
    // {
    //     List<FoodModel> existingFoodItems = FoodAccess.LoadAll();
    //     Console.WriteLine("Cinema food items:");
    //     Console.WriteLine("-----------------------------------");
    //     foreach (var foodItem in existingFoodItems)
    //     {
    //         Console.WriteLine(foodItem.Name);
    //         Console.WriteLine($"Price: {foodItem.Price}");
    //         Console.WriteLine("-----------------------------------");
    //     }
    // }

    // public static void PrintMoviesJson(string filePath)
    // {
    //     List<MovieModel> existingMovies = MovieAccess.LoadAll();
    //     Console.WriteLine("Cinema movies:");

    //     foreach (var movie in existingMovies)
    //     {
    //         Console.WriteLine(JsonSerializer.Serialize(movie));
    //     }

    // }

    //FillJsons.PrintFoodJson(@"DataSources/food.json");
    //FillJsons.PrintMoviesJson(@"DataSources/movies.json");
}