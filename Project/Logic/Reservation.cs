static class Reservation
{
    public static void ReserveMovie(string userInput = "TEST", int intUserAccountId = 1)
    {
        Console.WriteLine($"{userInput} + {intUserAccountId}");
        MovieLogic objMovieLogic = new();
        MovieModel foundMovie = null;
        if (userInput == "TEST")
        {
            foundMovie = objMovieLogic.GetBySearch("The Dummy Movie");
        }
        else
        {
            foundMovie = objMovieLogic.GetBySearch(userInput);
        }

        AccountsLogic objAccountLogic = new();
        AccountModel acc = objAccountLogic.GetByArg(intUserAccountId);

        bool reservationFound = false;

        foreach (ReservationModel resMod in acc.Reservations)
        {
            if (resMod.ObjMovieModel.Equals(foundMovie))
            {
                if (resMod.TicketsCount < 8)
                {
                    resMod.TicketsCount += 1;
                    reservationFound = true;
                    break; // No need to continue searching
                }
                else
                {
                    Console.WriteLine("Maximum number of seats reached for this movie.");
                    return;
                }
            }
        }

        if (!reservationFound)
        {
            int index = acc.Reservations.Count;
            ReservationModel reservationModel = new ReservationModel(index, foundMovie, 1);
            acc.Reservations.Add(reservationModel);
        }

        objAccountLogic.UpdateList(acc);
    }
}
