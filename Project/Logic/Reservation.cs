static class Reservation
{
    public static void ReserveMovie(string userInput="TEST")
    {
        if (userInput == "TEST")
        {
            MovieLogic objMovieLogic = new();
            MovieModel foundMovie = objMovieLogic.GetBySearch("The Dummy Movie");

            AccountsLogic objAccountLogic = new();
            AccountModel adminAccount = objAccountLogic.GetByArg(3);

            int index = adminAccount.Reservations.Count;  // This makes the index of the reservation
            ReservationModel reservationModel = null;

            if (index > 0)
            {
                foreach (ReservationModel resMod in adminAccount.Reservations)
                {
                    if (resMod.ObjMovieModel.Equals(foundMovie))
                    {
                        adminAccount.Reservations[index - 1].TicketsCount += 1;
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
}
