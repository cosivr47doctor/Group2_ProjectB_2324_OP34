using System.Text.RegularExpressions;
using System.Collections.Generic;

static class AddReservation
{
    static private FoodLogic foodLogic = new FoodLogic();
    static private MovieLogic movieLogic = new MovieLogic();
    static private Reservation reservation = new Reservation();
    private static List<ReservationModel> _reservations = GenericAccess<ReservationModel>.LoadAll();

    public static void UpdateList(ReservationModel resv)
    {
        // For auto-increment
        int maxId = _reservations.Count > 0 ? _reservations.Max(m => m.Id) : 0;
        //Find if there is already a model with the same id
        int index = _reservations.FindIndex(s => s.Id == resv.Id);

        if (index != -1)
        {
            _reservations[index] = resv;
        }
        else
        {
            resv.Id = maxId + 1;
            _reservations.Add(resv);
        }
        GenericAccess<ReservationModel>.WriteAll(_reservations);
    }

    public static (int, int) SelectSession(int movieId, int accId)
    {
        List<MovieScheduleModel> schedule = GenericAccess<MovieScheduleModel>.LoadAll();
        List<MovieModel> movies = GenericAccess<MovieModel>.LoadAll();

        // List<MovieScheduleModel>
        var matchingSchedules = schedule.Where(resv => resv.TimeIdPair.Values.First().Split(" ")[1].Trim() == Convert.ToString(movieId)).ToList();
        if (!matchingSchedules.Any())
        {
            Console.WriteLine("No sessions found for the selected movie.");
            UserMenu.Start(accId);
            // return (-1, -1); // Return some invalid default value
        }
        foreach (MovieScheduleModel session in matchingSchedules)
        {
            string idOfMovieStr = session.TimeIdPair.Values.FirstOrDefault();
            int idOfMovie = (int)Char.GetNumericValue(idOfMovieStr[idOfMovieStr.Length - 1]);
            MovieModel movie = movies.FirstOrDefault(m => m.Id == movieId);
            string date = session.Date.Date.ToString("yyyy-MM-dd");
            Console.WriteLine($"ID: {session.Id}, Date: {date}, Session time: {session.TimeIdPair.Keys.First()}, Room: {session.Room}");
        }

        int sessionId = Convert.ToInt32(ConsoleE.IntInput("Please select a session"));

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
                Console.Write("Enter the name or id of the movie: ");
                string userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Invalid input, try again.");
                    Thread.Sleep(1000);
                    continue;
                }

                MovieModel foundMovie = movieLogic.SelectForResv(userInput);
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
            AskForFood(accId, 1, 7, "101", 0, dummyAccId);
            Console.WriteLine("Reservation added for the dummy account. Check updated json.");
        }
    }
    public static void AskForFood(int accId, int sessionId, int movieId, string seatsStr, int price, int dummyAccId=-1)
    {
        AccountsLogic objAccountsLogic = new(); bool isAdmin = objAccountsLogic.GetByArg(accId).isAdmin;
        int accountId;
        if (dummyAccId == 3) accountId = accId;
        else accountId = accId;

        List<string> options = new(){
            "Yes",
            "No"
        };

        int selectedOption = DisplayFoodUtil.DisplayF(options);
        Console.WriteLine($"Would you like to add food?");
        switch (selectedOption)
        {
            case 0:
                addFoodResv(accountId, sessionId, movieId, seatsStr, price, dummyAccId);
                break;
            case 1: 
                Console.WriteLine("No");
                DateTime purchaseTime = DateTime.Now;
                ReservationModel newReservation = new ReservationModel(accountId, sessionId, movieId, seatsStr, new string[0] {}, price, purchaseTime);
                UpdateList(newReservation);
                Console.ResetColor();
                if (isAdmin) AdminMenu.Start(accId);
                else UserMenu.Start(accId);
                //ResvDetails.ResvConfirmation(intUserAccountId, index);
                break;
            default:
                throw new Exception("error");
        }
    }

    public static void addFoodResv(int accId, int sessionId, int movieId, string seatsStr, int price, int dummyAccId=-1)
    {
        AccountsLogic objAccountsLogic = new(); bool isAdmin = objAccountsLogic.GetByArg(accId).isAdmin;

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

            string[] foodArray = foundFood.Name.Split(",");
            decimal totalPrice = foundFood.Price * quantity + price;
            Console.WriteLine($"Total Price: {totalPrice}");
            DateTime purchaseTime = DateTime.Now;
            ReservationModel newReservation = new ReservationModel(accId, sessionId, movieId, seatsStr, foodArray, totalPrice, purchaseTime);
            reservation.UpdateList(newReservation);
            // SeeJsons.PrintLastResvGJson(@"DataSources/reservations.json");
            // Console.WriteLine("Press any key to continue");
            Console.WriteLine("Reservation added successfully");
            // Console.ReadLine();
            if (isAdmin) AdminMenu.Start(accId);
            else UserMenu.Start(accId);
            //ResvDetails.ResvConfirmation(intUserAccountId, index);
        }
    }
}