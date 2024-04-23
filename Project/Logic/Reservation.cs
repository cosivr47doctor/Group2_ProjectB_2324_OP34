static class Reservation
{
    public static void ReserveMovie(string userInput="TEST", int intUserAccountId = 3)
    {
        MovieLogic objMovieLogic = new();
        MovieModel foundMovie = null;
        if (userInput == "TEST") {foundMovie = objMovieLogic.GetBySearch("The Dummy Movie");}
        else {foundMovie = objMovieLogic.GetBySearch(userInput);}

        AccountsLogic objAccountLogic = new();
        AccountModel adminAccount = objAccountLogic.GetByArg(intUserAccountId);

        int index = adminAccount.Reservations.Count;  // This makes the index of the reservation
        ReservationModel reservationModel = null;

        if (index > 0)
        {
            foreach (ReservationModel resMod in adminAccount.Reservations)
            {
                if (resMod.ObjMovieModel.Equals(foundMovie))
                {
                    if (resMod.TicketsCount < 8)
                    {
                        adminAccount.Reservations[index - 1].TicketsCount += 1;
                    }
                    else
                    {
                        Console.WriteLine("Maximum number of seats reached for this movie.");
                        return;
                    }
                }
                else
                {
                    reservationModel = new(index, foundMovie, 1);
                    adminAccount.Reservations.Add(reservationModel);
                }
            }
        }
        else
        {
            reservationModel = new(index, foundMovie, 1);  // Index, MovieModel, TicketsCount
            adminAccount.Reservations.Add(reservationModel);
        }

        objAccountLogic.UpdateList(adminAccount);
    }
}
