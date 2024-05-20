static class ResvDetails
{
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

        foreach (int reservationId in account.ReservationIds)
        {
            Console.WriteLine(reservations.Where(resv => resv.Id == reservationId));
            Console.WriteLine("-----------------------------------");
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

        int lastReservationId = account.ReservationIds.FirstOrDefault(Id => Id == resvID);

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
}

