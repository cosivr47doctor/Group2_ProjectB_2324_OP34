static class ResvDetails
{
    public static void ResvHistory(int accountId)
    {
        Console.Clear();
        AccountsLogic accountsLogic = new();
        AccountModel account = accountsLogic.GetByArg(accountId);

        if (account == null)
        {
            Console.WriteLine($"Account with ID {accountId} not found.");
            return;
        }
        Console.WriteLine($"Email Address: {account.EmailAddress}");
        Console.WriteLine("Reservation History:");
        Console.WriteLine("-----------------------------------");

        foreach (var reservation in account.Reservations)
        {
            Console.WriteLine($"Movie Name: {reservation.Name}");
            Console.WriteLine($"Tickets Count: {reservation.TicketsCount}");
            Console.WriteLine($"Price: {reservation.Price}");
            Console.WriteLine($"Seats: -");
            Console.WriteLine($"Date: -");
            Console.WriteLine($"Time: -");
            Console.WriteLine("-----------------------------------");
        }
    }
    public static void ResvConfirmation(int accountId, int resvID)
    {
        Console.Clear();
        AccountsLogic accountsLogic = new();
        AccountModel account = accountsLogic.GetByArg(accountId);

        if (account == null)
        {
            Console.WriteLine($"Account with ID {accountId} not found.");
            return;
        }

        Console.WriteLine($"Email Address: {account.EmailAddress}");
        Console.WriteLine("Reservation receipt:");
        Console.WriteLine("-----------------------------------");

        ReservationModel lastReservation = account.Reservations.FirstOrDefault(res => res.Id == resvID);

        if (lastReservation != null)
        {
            Console.WriteLine($"Movie Name: {lastReservation.Name}");
            Console.WriteLine($"Tickets Count: {lastReservation.TicketsCount}");
            Console.WriteLine($"Price: {lastReservation.Price}");
            Console.WriteLine($"Seats: -");
            Console.WriteLine($"Date: -");
            Console.WriteLine($"Time: -");
            Console.WriteLine("-----------------------------------");
        }
        else
        {
            Console.WriteLine("No reservations found.");
        }
    }
}

