using Newtonsoft.Json;
using System;
public static class SeeJsons
{
    public static void PrintFoodJson(string filePath)
    {
        FoodLogic foodLogic = new FoodLogic();
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
        MovieLogic movieLogic = new MovieLogic();
        Console.Clear();
        Console.WriteLine("Cinema movies:");
        Console.WriteLine(new string('-', 123));
        Console.WriteLine("|  ID  |           Title           |  Year  |           Genre                |       Director                 |  Duration |  ");
        Console.WriteLine(new string('-', 123));

        foreach (var movie in movieLogic.Movies)
        {
            string genres = string.Join(", ", movie.Genre);
            string movieDetails = $"| {movie.Id,4} | {movie.Name,-25} | {movie.Year,6} | {genres,30} | {movie.Director,30} | {movie.Duration,9} |";
            Console.WriteLine(movieDetails);
            Console.WriteLine(new string('-', 123));
        }
    }


    public static void PrintSchedulesJson(string filePath, DateTime? fromDate = null, DateTime? untilDate = null)
    {
        MovieSchedulingLogic movieSchedulingLogic = new MovieSchedulingLogic();
        ConsoleE.Clear();
        Console.WriteLine("Movies schedule:");
        Console.WriteLine(new string('-', 120));

        if (fromDate is null && untilDate is null)
        {
            foreach (var schedule in movieSchedulingLogic.movieSchedules)
            {
                Console.WriteLine(schedule.ToString());
            }
        }
        else
        {
            var sessionsOnDate = movieSchedulingLogic.movieSchedules.Where(ms => ms.Date >= fromDate.Value && ms.Date <= untilDate.Value).ToList();
            foreach (var schedule in sessionsOnDate)
            {
                Console.WriteLine(schedule.ToString());
            }
            if (sessionsOnDate.Count == 0) Console.WriteLine($"No sessions between the specified dates {fromDate.Value} & {untilDate.Value} available.");
            else if (untilDate.Value > movieSchedulingLogic.movieSchedules.Last().Date)
            {
                string date = movieSchedulingLogic.movieSchedules.Last().Date.Date.ToString("yyyy-MM-dd");
                Console.WriteLine($"Error: no new sessions scheduled after {date}");
 
            }
        }
    }
    
    //FillJsons.PrintFoodJson(@"DataSources/food.json");

    public static void PrintLastResvGJson(string filePath)
    {
        Reservation reservationLogic = new Reservation();
        ConsoleE.Clear();
        Console.WriteLine("Last Reservation details:");
        Console.WriteLine(new string('-', 120));
 
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
 
        Console.WriteLine(new string('-', 120));
    }
}