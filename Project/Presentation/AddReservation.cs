using System.Text.RegularExpressions;
using System.Collections.Generic;

static class AddReservation
{
    static private FoodLogic foodLogic = new FoodLogic();
    static private MovieLogic movieLogic = new MovieLogic();
    static private Reservation reservation = new Reservation();

    public static void addMovieResv()
    {
        while (true)
        {
            Console.Clear();
            Console.ResetColor();
            SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
            Console.WriteLine("");
            //zie welke films je kan kiezen
            Console.Write("Enter the name or id of the movie: ");
            string userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Invalid input, try again.");
                Thread.Sleep(1000);
                continue;
            }

            MovieModel foundMovie = movieLogic.SelectForResv(userInput);

            if (foundMovie == null)
            {
                Console.WriteLine("Movie not found, try again.");
                Thread.Sleep(1000);
                continue;
            }
            Console.WriteLine($"Movie found: {foundMovie.Name}");
            Console.WriteLine("Heading to the room seats");
            Thread.Sleep(3000);
            RoomSeats.Room1(foundMovie.Id);
        }
    }
    public static void AskForFood(int price, string seats, int foundMovie)
    {

        List<string> options = new(){
            "Yes",
            "No"
        };

        int selectedOption = DisplayFoodUtil.DisplayF(options);
        Console.WriteLine($"Would you like to add food?");
        switch (selectedOption)
        {
            case 0:
                addFoodResv(price, seats, foundMovie);
                break;
            case 1: 
                Console.WriteLine("No");
                ReservationModel2 newReservation = new ReservationModel2(foundMovie, seats, new string[0] {}, price);
                reservation.UpdateList(newReservation);
                Console.ResetColor();
                UserMenu.Start();
                //ResvDetails.ResvConfirmation(intUserAccountId, index);
                break;
            default:
                throw new Exception("error");
        }
    }

    public static void addFoodResv(int price, string seats, int foundMovie)
    {

        while (true)
        {
            SeeJsons.PrintFoodJson(@"DataSources/food.json");
            Console.WriteLine("");
            Console.Write("Enter the name or id of the food you like to order: ");
            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Please enter the name or id of the food.");
                continue;
            }

            FoodModel foundFood = foodLogic.SelectForResv(userInput);

            if (foundFood == null)
            {
                Console.WriteLine("Food not found.");
                continue;
            }

            Console.WriteLine($"Selected food: {foundFood.Name}, Price: {foundFood.Price}");

            int quantity;
            while (true)
            {
                Console.Write("Enter the amount: ");
                string quantityInput = Console.ReadLine();
                if (int.TryParse(quantityInput, out quantity) && quantity > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid amount.");
                }
            }

            string[] foodName = foundFood.Name.Split(",");
            decimal totalPrice = foundFood.Price * quantity + price;
            Console.WriteLine($"Total Price: {totalPrice}");
            ReservationModel2 newReservation = new ReservationModel2(foundMovie, seats, foodName, totalPrice);
            reservation.UpdateList(newReservation);
            // SeeJsons.PrintLastResvGJson(@"DataSources/reservations.json");
            // Console.WriteLine("Press any key to continue");
            Console.WriteLine("Reservation added successfully");
            // Console.ReadLine();
            UserMenu.Start();
            //ResvDetails.ResvConfirmation(intUserAccountId, index);
        }
    }
}