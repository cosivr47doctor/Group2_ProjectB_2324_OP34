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
            
            int index = adminAccount.Reservations.Count;
            string movieTitle = $"Id: {foundMovie.Id}, Title: {foundMovie.Name}";

            adminAccount.Reservations[index] = movieTitle;
            objAccountLogic.UpdateList(adminAccount);
        }
    }
}
