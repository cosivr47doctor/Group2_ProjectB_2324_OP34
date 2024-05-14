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
        Console.Clear();
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
        Console.Clear();
        Console.WriteLine("Cinema movies:");
        Console.WriteLine("---------------------------------------------------------------------------------------------------");
        Console.WriteLine("---------------------------------------------------------------------------------------------------");
        Console.WriteLine("|  ID  |           Title          |  Year  |          Genre          |       Director      |  Duration  |");
        Console.WriteLine("---------------------------------------------------------------------------------------------------");

        foreach (var movie in movieLogic.Movies)
        {
            string movieDetails = $"| {movie.Id,4} | {movie.Name,-25} | {movie.Year,6} | {string.Join(", ", movie.Genre),23} | {movie.Director,-20} | {movie.Duration,9} |";
            Console.WriteLine(movieDetails);
            Console.WriteLine("-----------------------------------");
        }
    }

    public static void PrintSchedulesJson(string filePath, DateTime? fromDate = null, DateTime? untilDate = null)
    {
        Console.Clear();
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
            Console.WriteLine($"Seats: {resvA.Seats}");
            Console.WriteLine($"Food: {string.Join(", ", resvA.Food)}");
            Console.WriteLine($"Total price: {resvA.TotalPrice}");
            Console.WriteLine("-----------------------------------");
        }
    }
}