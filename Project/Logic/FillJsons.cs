using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class FillJsons
{
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
                existingFoodItems.Add(item);
            }
        }
        FoodAccess.WriteAll(existingFoodItems);
    }

    public static void FillMoviesJson(string filePath)
    {
        List<MovieModel> existingMovies = MovieAccess.LoadAll();

        List<MovieModel> standardMovies = new List<MovieModel>
        {
            new MovieModel("Inception", "Science Fiction", "A thief who enters the dreams of others to steal their secrets from their subconscious.", "Christopher Nolan", 148),
            new MovieModel("The Shawshank Redemption", "Drama", "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", "Frank Darabont", 142),
            new MovieModel("The Godfather", "Crime", "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", "Francis Ford Coppola", 175),
            new MovieModel("The Matrix", "Action", "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", "Lana Wachowski, Lilly Wachowski", 136)
        };

        foreach (var item in standardMovies)
        {
            int index = existingMovies.FindIndex(movie => movie.Name == item.Name);

            if (index == -1)
            {
                existingMovies.Add(item);
            }
        }
        MovieAccess.WriteAll(existingMovies);
    }
}