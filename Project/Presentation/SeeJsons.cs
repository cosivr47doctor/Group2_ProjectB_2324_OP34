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
        ConsoleE.Clear();
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
        ConsoleE.Clear();
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
        ConsoleE.Clear();
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

        public static void PrintLastResvGJson(string filePath)
    {
        ConsoleE.Clear();
        Console.WriteLine("Last Reservation details:");
        Console.WriteLine("-----------------------------------");
 
        if (reservationLogic.Reservations.Count > 0)
        {
            var lastReservation = reservationLogic.Reservations.Last();
            Console.WriteLine($"Id: {lastReservation.Id}");
            Console.WriteLine($"Seats: {lastReservation.Seats}");
            Console.WriteLine($"Food: {string.Join(", ", lastReservation.Food)}");
            Console.WriteLine($"Total price: {lastReservation.TotalPrice}");
        }
        else
        {
            Console.WriteLine("No reservations found.");
        }
 
        Console.WriteLine("-----------------------------------");
    }
}