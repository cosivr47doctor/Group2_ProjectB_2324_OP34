using Newtonsoft.Json;
using System;
public static class SeeJsons
{
    static private FoodLogic foodLogic = new FoodLogic();
    static private MovieLogic movieLogic = new MovieLogic();
    static private Reservation reservationLogic = new Reservation();
    static private MovieSchedulingLogic movieSchedulingLogic = new MovieSchedulingLogic();
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
            Console.WriteLine($"Genre: {string.Join(", ", movie.Genre)}");
            Console.WriteLine($"Year: {movie.Year}");
            Console.WriteLine($"Description: {movie.Description}");
            Console.WriteLine($"Director: {movie.Director}");
            Console.WriteLine($"Duration (in minutes): {movie.Duration}");
            Console.WriteLine("-----------------------------------");
        }
    }

    public static void PrintSchedulesJson(string filePath, DateTime? fromDate = null, DateTime? untilDate = null)
    {
        Console.WriteLine("Movies schedule:");
        Console.WriteLine("-----------------------------------");

        if (fromDate is null && untilDate is null)
        {
            foreach (var schedule in movieSchedulingLogic.movieSchedules)
            {
                Console.WriteLine(schedule.ToString());
            }
        }
        else
        {
            foreach (var schedule in movieSchedulingLogic.movieSchedules)
            {
                if (schedule.Date >= fromDate.Value && schedule.Date <= untilDate.Value)
                {
                    Console.WriteLine(schedule.ToString());
                }
            }
        }
    }
    
    //FillJsons.PrintFoodJson(@"DataSources/food.json");

    public static void PrintResvGJson(string filePath)
    {
        Console.Clear();
        Console.WriteLine("Reservation details (seats):");
        Console.WriteLine("-----------------------------------");

        foreach (var resvA in reservationLogic.Reservations)
        {
            Console.WriteLine($"Id: {resvA.Id}");
            Console.WriteLine($"Name: {resvA.Seats}");
            Console.WriteLine($"Genre: {string.Join(", ", resvA.Food)}");
            Console.WriteLine($"Year: {resvA.TotalPrice}");
            Console.WriteLine("-----------------------------------");
        }
    }
}