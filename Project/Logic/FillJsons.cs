using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class FillJsons
{
    public static void FillAccountsJson(string filePath)
    {
        List<AccountModel> existingAccountItems = GenericAccess<AccountModel>.LoadAll();
    
        List<AccountModel> standardItems = new List<AccountModel>
        {

        };
    
        foreach (var item in standardItems)
        {
            int index = existingAccountItems.FindIndex(account => account.EmailAddress == account.EmailAddress);

            if (index == -1)
            {
                existingAccountItems.Insert(0, item);
            }
        }
        GenericAccess<AccountModel>.WriteAll(existingAccountItems);
    }

    public static void FillFoodJson(string filePath)
    {
        List<FoodModel> existingFoodItems = GenericAccess<FoodModel>.LoadAll();

        List<FoodModel> standardItems = new List<FoodModel>
        {
            new FoodModel("popcorn", 2.99m),
            new FoodModel("chips", 3.50m),
            new FoodModel("cola", 2.50m)
        };

        foreach (var item in standardItems)
        {
            GenericMethods.UpdateList(item);
        }
    }

    public static void FillMoviesJson(string filePath)
    {
        List<MovieModel> standardMovies = new List<MovieModel>
        {
            new MovieModel("Inception", new[] {"Science Fiction"}, 2010, "A thief who enters the dreams of others to steal their secrets from their subconscious.", "Christopher Nolan", 148),
            new MovieModel("The Shawshank Redemption", new[] {"Drama"}, 1994, "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", "Frank Darabont", 142),
            new MovieModel("The Godfather", new[] {"Crime"}, 1972, "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", "Francis Ford Coppola", 175),
            new MovieModel("The Matrix", new[] {"Action"}, 1999, "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", "Lana Wachowski, Lilly Wachowski", 136)
        };

        foreach (var item in standardMovies)
        {
            GenericMethods.UpdateList(item);
        }
    }
}