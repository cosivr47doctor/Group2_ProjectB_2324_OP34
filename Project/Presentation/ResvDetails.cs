static class ResvDetails
{

    static private Reservation reservationLogic = new Reservation();
    public static void ResvHistory(int accountId)
    {
        Console.Clear();
        AccountsLogic accountsLogic = new();
        AccountModel account = accountsLogic.GetByArg(accountId);
        List<ReservationModel> reservations = GenericAccess<ReservationModel>.LoadAll();

        if (account == null)
        {
            Console.WriteLine($"Account with ID {accountId} not found.");
            return;
        }
        Console.WriteLine($"Email Address: {account.EmailAddress}");
        Console.WriteLine("Reservation History:");
        Console.WriteLine("-----------------------------------");

        foreach (ReservationModel resv in reservations)
        {
            if (resv.AccountId == account.Id)
            {  
                Console.WriteLine(resv.ToString());
                Console.WriteLine("-----------------------------------");
            }
        }
    }
    public static void ResvConfirmation(int accountId, int resvID)
    {
        Console.Clear();
        AccountsLogic accountsLogic = new();
        AccountModel account = accountsLogic.GetByArg(accountId);
        List<ReservationModel> reservations = GenericAccess<ReservationModel>.LoadAll();

        if (account == null)
        {
            Console.WriteLine($"Account with ID {accountId} not found.");
            return;
        }

        Console.WriteLine($"Email Address: {account.EmailAddress}");
        Console.WriteLine("Reservation receipt:");
        Console.WriteLine("-----------------------------------");

        int lastReservationId = reservations.Where(resv => resv.AccountId == account.Id).Last().Id;

        if (lastReservationId != null)
        {
            Console.WriteLine(reservations.Where(resv => resv.Id == lastReservationId));
            //Console.WriteLine($"Time: -");
            Console.WriteLine("-----------------------------------");
        }
        else
        {
            Console.WriteLine("No reservations found.");
        }
    }

    public static void ResvReceipt(string filePath)
    {
        Console.Clear();
        Console.WriteLine("Reservation conformation");
        Console.WriteLine("---------------------------------------------------------------------------------------------------");
        Console.WriteLine("---------------------------------------------------------------------------------------------------");
        Console.WriteLine("|       Date        |           Title          |            Seats           |         Selected food          |       Price      |");
        Console.WriteLine("---------------------------------------------------------------------------------------------------");

        foreach (var reservation in reservationLogic.Reservations)
        {
            string resvDetails = $"| {reservation.MovieId,4} | {reservation.MovieId,-25} | {reservation.Seats,6} | {string.Join(", ", reservation.Food),23} | {reservation.TotalPrice,9} |";
            Console.WriteLine(resvDetails);
            Console.WriteLine("-----------------------------------");
        }
    }
}

