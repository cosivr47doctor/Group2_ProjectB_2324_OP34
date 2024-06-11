using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

static class AddReservation
{
    static private FoodLogic foodLogic = new FoodLogic();
    static private MovieLogic movieLogic = new MovieLogic();

    public static (int, int) SelectSession(int movieId, int accId)
    {
        List<MovieScheduleModel> schedule = GenericAccess<MovieScheduleModel>.LoadAll();
        List<MovieModel> movies = GenericAccess<MovieModel>.LoadAll();

        // List<MovieScheduleModel>
        var matchingSchedules = schedule.Where(resv => resv.MovieId == movieId).ToList();
        if (!matchingSchedules.Any())
        {
            Console.WriteLine("No sessions found for the selected movie.");
            UserMenu.Start(accId);
            // return (-1, -1); // Return some invalid default value
        }
        matchingSchedules = matchingSchedules.OrderBy(s => s.Room).ToList();

        int previousRoom = -1;
        foreach (MovieScheduleModel session in matchingSchedules)
        {
            if (session.Room != previousRoom && previousRoom != -1)
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
            }

            if (session.Room == 1 && previousRoom != 1)
            {
                Console.WriteLine("---------------------------------------------------------------------");
            }
            
            previousRoom = session.Room;
            string idOfMovieStr = session.TimeIdPair.Values.FirstOrDefault();
            int idOfMovie = (int)Char.GetNumericValue(idOfMovieStr[idOfMovieStr.Length - 1]);
            MovieModel movie = movies.FirstOrDefault(m => m.Id == movieId);
            string date = session.Date.Date.ToString("yyyy-MM-dd");
            Console.WriteLine($"ID: {session.Id}, Date: {date}, Session time: {session.TimeIdPair.Keys.First()}, Room: {session.Room}");

            Console.WriteLine();
        }
        if (matchingSchedules.Any())
        {
            Console.WriteLine("---------------------------------------------------------------------");
        }

        int sessionId = Convert.ToInt32(ConsoleE.IntInput("Please select a session ID"));

        if (sessionId < 0 || !matchingSchedules.Any(resv => resv.Id == sessionId))
        {
            Console.WriteLine("Invalid input or session ID not found.");
            UserMenu.Start(accId);
        }
        int roomId = schedule.Where(resv => resv.Id == sessionId).Select(resv => resv.Room).FirstOrDefault();
        if (roomId == 0)
        {
            Console.WriteLine("Invalid session selected.");
            UserMenu.Start(accId);
        }

        return (sessionId, roomId);
    }

    public static void addMovieResv(int accId = -1, int dummyAccId=-1)
    {
        if (accId < 0)
        {
            Console.WriteLine("Invalid account ID error");
            MainMenu.Start();
        }
        if (dummyAccId != 3)
        {
            while (true)
            {
                Console.Clear();
                Console.ResetColor();
                SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
                Console.WriteLine("");
                //zie welke films je kan kiezen
                Console.Write("Enter the name or id of the movie or [Q] to go back: ");
                string userInput = Console.ReadLine();
                if (ConsoleE.BackContains(userInput)) UserMenu.Start(accId);
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Invalid input, try again.");
                    Thread.Sleep(1000);
                    continue;
                }

                MovieModel foundMovie = movieLogic.SelectForResv(userInput);
                if (foundMovie == null) {Console.WriteLine("Invalid movie id input"); return;};
                (int, int) sessionIdAndRoomId = SelectSession(foundMovie.Id, accId);

                if (foundMovie == null)
                {
                    Console.WriteLine("Movie not found, try again.");
                    Thread.Sleep(1000);
                    continue;
                }
                Console.WriteLine($"Movie found: {foundMovie.Name}");
                Console.WriteLine("Heading to the room seats");
                Thread.Sleep(3000);
                RoomSeats.SelectRoom(accId, sessionIdAndRoomId.Item1, foundMovie.Id, sessionIdAndRoomId.Item2);
            }
        }
        else
        {
            // MovieModel dummyMovie = movieLogic.SelectForResv("3");
            AskForFood(accId, 1, 7, "101", 0, new TakenSeatsModel(0, "test", 1, 1, new List<int>{1}), dummyAccId);
            Console.WriteLine("Reservation added for the dummy account. Check updated json.");
        }
    }
    public static void AskForFood(int accId, int sessionId, int movieId, string seatsStr, decimal price, TakenSeatsModel roomDetails=null, int dummyAccId=-1)
    {
        AccountsLogic objAccountsLogic = new(); bool isAdmin = objAccountsLogic.GetByArg(accId).isAdmin;
        int accountId;
        if (dummyAccId == 3) accountId = accId;
        else accountId = accId;

        List<string> options = new(){
            "Yes",
            "No",
            "Cancel"
        };

        int selectedOption = DisplayUtil.DisplayAddFood(options);
        Console.WriteLine($"Would you like to add food?");
        if (selectedOption == 0)
        {
            addFoodResv(accountId, sessionId, movieId, seatsStr, price, roomDetails, dummyAccId);
        }
        else if (selectedOption == 1)
        {
            GenericMethods.UpdateList(roomDetails);
            Console.WriteLine("No");
            DateTime purchaseTime = DateTime.Now;
            ReservationModel newReservation = new ReservationModel(roomDetails.ReservationCode, accountId, sessionId, movieId, seatsStr, new string[0] {}, price, purchaseTime);
            EmailConf.GenerateEmailBodyNoFood(accountId, newReservation);
            GenericMethods.UpdateList(newReservation);
            Console.ResetColor();
            Console.WriteLine("Reservation added successfully");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Console.Clear();
            if (isAdmin) AdminMenu.Start(accId);
            else UserMenu.Start(accId);
            //ResvDetails.ResvConfirmation(intUserAccountId, index);
        }
        else if (selectedOption == 2)
        {
            Console.WriteLine("Reservation cancelled.");
            Thread.Sleep(2500);
            UserMenu.Start(accId);
        }
         else throw new Exception("error");
    }

    public static void addFoodResv(int accId, int sessionId, int movieId, string seatsStr, decimal price, TakenSeatsModel roomDetails, int dummyAccId=-1)
    {
        AccountsLogic objAccountsLogic = new(); bool isAdmin = objAccountsLogic.GetByArg(accId).isAdmin;
        List<(FoodModel food, int quantity)> selectedFoods = new();
        do
        {
            SeeJsons.PrintFoodJson(@"DataSources/food.json");
            Console.WriteLine("");
            Console.Write("Enter the name or id of the food you like to order or [Q] to go back: ");
            string userInput = Console.ReadLine();

            if (ConsoleE.BackContains(userInput)) 
            {
                AskForFood(accId, sessionId, movieId, seatsStr, price, roomDetails, dummyAccId=-1);
                return;
            }

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
                Console.Write("Enter the amount: [Q to go back]");
                string quantityInput = Console.ReadLine();
                if (ConsoleE.BackContains(quantityInput)) 
                {
                    AskForFood(accId, sessionId, movieId, seatsStr, price, roomDetails, dummyAccId=-1);
                    return;
                }
                if (int.TryParse(quantityInput, out quantity) && quantity > 0)
                {
                    selectedFoods.Add((foundFood, quantity));
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid amount.");
                }
            }
        } while (PromptOrderMore());

        // Finalize the order if there are selected foods
        if (selectedFoods.Count > 0)
        {
            List<string> foodNames = new List<string>();
            decimal totalPrice = price;

            foreach ((FoodModel food, int quantity) in selectedFoods)
            {
                for (int i = 0; i < quantity; i++)
                {
                    foodNames.Add(food.Name);
                }
                totalPrice += food.Price * quantity;
            }
            Console.WriteLine($"Total Price: {totalPrice}");
            DateTime purchaseTime = DateTime.Now;
            ReservationModel newReservation = new ReservationModel(roomDetails.ReservationCode, accId, sessionId, movieId, seatsStr, foodNames.ToArray(), totalPrice, purchaseTime);
            EmailConf.GenerateEmailBody(accId, newReservation);
            GenericMethods.UpdateList(newReservation);
            Console.WriteLine("Reservation added successfully");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Console.Clear();

        }

        GenericMethods.UpdateList(roomDetails);
        if (isAdmin) 
        {
            AdminMenu.Start(accId);
        }
        else 
        {
            UserMenu.Start(accId);
        }
    }

    private static bool PromptOrderMore()
    {
        Console.Write("Do you want to order more food? (Y/N): ");
        string moreFoodInput = Console.ReadLine().Trim().ToUpper();
        return moreFoodInput == "Y";
    }

    public static void CancelReservation(int accountId)
    {
        List<ReservationModel> reservations = GenericAccess<ReservationModel>.LoadAll();
        List<TakenSeatsModel> rooms = GenericAccess<TakenSeatsModel>.LoadAll();
        bool validInput = false;
        while (!validInput)
        {
            Console.WriteLine("Enter the reservation code of the reservation you want to cancel: [Q to go back]");
            string reservationCodeInput = Console.ReadLine();

            if (ConsoleE.BackContains(reservationCodeInput))
            {
                return;
            }

            ReservationModel reservationToCancel = reservations.FirstOrDefault(resv => resv.ReservationCode == reservationCodeInput && resv.AccountId == accountId);

            if (reservationToCancel != null)
            {
                MovieScheduleModel movieSched = GenericAccess<MovieScheduleModel>.LoadAll().Where(ms => ms.Id == reservationToCancel.SessionId).First();
                DateTime now = DateTime.Now;
                string[] schedSession = movieSched.TimeIdPair.Keys.First().Split(" - ");
                DateTime scheduleDateTime = movieSched.Date.Add(DateTime.Parse(schedSession[0]).TimeOfDay);
                if (now.Date >= scheduleDateTime)
                {
                    Console.WriteLine("Can't cancel: date has expired already.");
                    return;
                }
                reservations.Remove(reservationToCancel);
                // Find all rooms with the same reservation code and remove them
                List<TakenSeatsModel> roomsToCancel = rooms.Where(room => room.ReservationCode == reservationCodeInput).ToList();
                foreach (var room in roomsToCancel)
                {
                    rooms.Remove(room);
                }
                // Save the updated lists back to the file
                GenericAccess<ReservationModel>.WriteAll(reservations);
                GenericAccess<TakenSeatsModel>.WriteAll(rooms);

                Console.WriteLine("Reservation cancelled successfully.");
                validInput = true;
            }
            else
            {
                Console.WriteLine("Reservation not found or you do not have permission to cancel this reservation.");
                validInput = true;
            }
        }
    }

    /*public static void CancelReservation(int accountId)
    {
        List<ReservationModel> reservations = GenericAccess<ReservationModel>.LoadAll();
        List<RoomModel> rooms = GenericAccess<RoomModel>.LoadAll();
        bool validInput = false;
        while (!validInput)
        {
            Console.WriteLine("Enter the reservation code of the reservation you want to cancel:");
            string sessionInput = Console.ReadLine();
            int sessionId;

            if (int.TryParse(sessionInput, out sessionId))
            {
                ReservationModel reservationToCancel = reservations.FirstOrDefault(resv => resv.SessionId == sessionId && resv.AccountId == accountId);
                RoomModel roomToCancel = rooms.FirstOrDefault(room => room.SessionId == sessionId);
            
            if (reservationToCancel != null)
            {
                reservations.Remove(reservationToCancel);
                rooms.Remove(roomToCancel);
                // Save the updated list back to the file
                GenericAccess<ReservationModel>.WriteAll(reservations);
                GenericAccess<RoomModel>.WriteAll(rooms);
                

                Console.WriteLine("Reservation cancelled successfully.");
                validInput = true;
                }
                else
                {
                    Console.WriteLine("Reservation not found or you do not have permission to cancel this reservation.");
                    validInput = true;
                }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid reservation code.");
            }
        }
    }*/
}
