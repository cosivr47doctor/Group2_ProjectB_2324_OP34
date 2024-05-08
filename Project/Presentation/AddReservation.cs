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
            //zie welke films je kan kiezen
            Console.Write("Enter the name or id of the movie: ");
            string userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Invalid input, please enter the name or id of the movie.");
                return;
            }

            MovieModel foundMovie = movieLogic.SelectForResv(userInput);

            if (foundMovie == null)
            {
                Console.WriteLine("Movie not found.");
                return;
            }
            Console.WriteLine($"Movie found: {foundMovie.Name}");
            Console.WriteLine("Heading to the room seats");
            Thread.Sleep(3000);
            RoomSeats.Room1(foundMovie.Id);
        }
    }
    public static void AskForFood(int price, string seats, int foundMovie)
    {

        Console.WriteLine($"Would you like to add food?");
        List<string> options = new(){
            "Yes",
            "No"
        };

        int selectedOption = DisplayUtil.Display(options);
        switch (selectedOption)
        {
            case 0:
                addFoodResv(price, seats, foundMovie);
                break;
            case 1: 
                Console.WriteLine("No");
                ReservationModel2 newReservation = new ReservationModel2(foundMovie, seats, null, price);
                reservation.UpdateList(newReservation);
                Console.ResetColor();
                MainMenu.Start();
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
            Console.Write("Enter the name or id of the food you like to order: ");
            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Please enter the name or id of the food.");
                return;
            }

            FoodModel foundFood = foodLogic.SelectForResv(userInput);

            if (foundFood == null)
            {
                Console.WriteLine("Food not found.");
                return;
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
            Console.WriteLine("Done makeing resv boyyy");
            MainMenu.Start();
            //ResvDetails.ResvConfirmation(intUserAccountId, index);
        }
    }
}