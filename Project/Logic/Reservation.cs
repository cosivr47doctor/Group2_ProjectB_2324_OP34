using System.Collections.Generic;
class Reservation
{

    private List<ReservationModel2> _reservation;
    public List<ReservationModel2> Reservations => _reservation;
    public Reservation()
    {
        _reservation = ReservationAccess.LoadAll();
    }

    public void UpdateList(ReservationModel2 newReservation)
    {
        _reservation.Add(newReservation);
        ReservationAccess.WriteAll(_reservation);
    }

    // public static void ReserveMovie(int intUserAccountId = 1)
    // {
    //     Console.Write("Enter the name or id of the movie: ");
    //     string userInput = Console.ReadLine();

    //     if (string.IsNullOrEmpty(userInput))
    //     {
    //         Console.WriteLine("Please enter the name or id of the movie.");
    //         return;
    //     }

    //     MovieLogic objMovieLogic = new();
    //     MovieModel foundMovie = objMovieLogic.SelectForResv(userInput);

    //     if (foundMovie == null)
    //     {
    //         Console.WriteLine("Movie not found.");
    //         return;
    //     }

    //     AccountsLogic objAccountLogic = new();
    //     AccountModel acc = objAccountLogic.GetByArg(intUserAccountId);

    //     int ticketCount;
    //     while (true)
    //     {
    //         Console.WriteLine("Price per ticket: 12,-");
    //         Console.Write("Enter the amount of tickets (1-8): ");
    //         string ticketInput = Console.ReadLine();
    //         if (int.TryParse(ticketInput, out ticketCount))
    //         {
    //             break;
    //         }
    //         else
    //         {
    //             Console.WriteLine("Invalid input, enter a valid number (1-8)");
    //         }
    //     }

    //     //foreach (ReservationModel resMod in acc.Reservations)
        
    //         //if (resMod.ObjMovieModel.Equals(foundMovie))
    //     bool reservationFound = false;
    //     if (ticketCount <= 8)
    //     {
    //         reservationFound = true;
    //     }
    //     else
    //     {
    //         Console.WriteLine("Maximum number of seats reached for this movie.");
    //         return;
    //     }
        
        

    //     if (reservationFound)
    //     {
    //         decimal price = ticketCount * 12;
    //         int index = acc.Reservations.Count + 1;
    //         ReservationModel reservationModel = new ReservationModel(index, foundMovie.Name, ticketCount, price, null);
    //         acc.Reservations.Add(reservationModel);
    //         objAccountLogic.UpdateList(acc);
    //         Console.WriteLine("Reservation successfull");
    //         Console.WriteLine("");
    //         Console.WriteLine("Press enter to see the reservation confirmation");
    //         Console.ReadLine();
    //         ResvDetails.ResvConfirmation(intUserAccountId, index);
    //     }
    //     else
    //     {
    //         Console.WriteLine("error, something went wrong");
    //     }
    // }

    // public static void ReserveFood(int intUserAccountId = 1)
    // {
    //     Console.Write("Enter the name or id of the food you like to order: ");
    //     string userInput = Console.ReadLine();

    //     if (string.IsNullOrEmpty(userInput))
    //     {
    //         Console.WriteLine("Please enter the name or id of the food.");
    //         return;
    //     }

    //     FoodLogic objFoodLogic = new();
    //     FoodModel foundFood = objFoodLogic.SelectForResv(userInput);

    //     if (foundFood == null)
    //     {
    //         Console.WriteLine("Food not found.");
    //         return;
    //     }

    //     Console.WriteLine($"Selected food: {foundFood.Name}, Price: {foundFood.Price}");

    //     int quantity;
    //     while (true)
    //     {
    //         Console.Write("Enter the quantity: ");
    //         string quantityInput = Console.ReadLine();
    //         if (int.TryParse(quantityInput, out quantity) && quantity > 0)
    //         {
    //             break;
    //         }
    //         else
    //         {
    //             Console.WriteLine("Invalid input. Please enter a valid quantity.");
    //         }
    //     }

    //     decimal totalPrice = foundFood.Price * quantity;
    //     Console.WriteLine($"Total Price: {totalPrice}");




    //     AccountsLogic objAccountLogic = new();
    //     AccountModel acc = objAccountLogic.GetByArg(intUserAccountId);
    //     int index = acc.Reservations.Count + 1;
    //     ReservationModel foodReservation = new ReservationModel(index, foundFood, totalPrice);
    //     acc.Reservations.Add(foodReservation);
    //     objAccountLogic.UpdateList(acc);
    //     Console.WriteLine("Food reservation successful.");


    // }
}