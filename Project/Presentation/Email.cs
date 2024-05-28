using static SeeJsons;
 
static class EmailConf
{
    public static void GenerateEmailBody(AccountModel account, ReservationModel reservation)
    {
        Console.Clear();
        MovieModel movie = GenericAccess<MovieModel>.LoadAll().Where(m => m.Id == reservation.Id).ToList().FirstOrDefault();
        Console.WriteLine(
$@"
------------------------------------------------------------------------
|Dear {account.FullName},                                                      |
|                                                                        |
|Thank you for your reservation. Here are your reservation details:      |
|                                                                        |
|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~                        |
|Reservation ID: {reservation.Id}  
|Movie: {movie.Name}                                                   
|Date: {reservation.PurchaseTime}
|Seat(s): {string.Join(", ", reservation.Seats)}
|Total price: {reservation.TotalPrice }
|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~                        |
|Enjoy your movie!                                                       |
|                                                                        |
|                                                                        |
|Best regards,                                                           |
|                                                                        |
|Team                                                                    |
|    ██████╗  ██████╗ ██╗   ██╗███████╗ ██████╗ ██████╗  ██████╗ ██████╗ |
|    ██╔══██╗██╔═══██╗╚██╗ ██╔╝██╔════╝██╔════╝██╔═══██╗██╔═══██╗██╔══██╗|
|    ██████╔╝██║   ██║ ╚████╔╝ ███████╗██║     ██║   ██║██║   ██║██████╔╝|
|    ██╔══██╗██║   ██║  ╚██╔╝  ╚════██║██║     ██║   ██║██║   ██║██╔═══╝ |
|    ██████╔╝╚██████╔╝   ██║   ███████║╚██████╗╚██████╔╝╚██████╔╝██║     |
|    ╚═════╝  ╚═════╝    ╚═╝   ╚══════╝ ╚═════╝ ╚═════╝  ╚═════╝ ╚═╝     |
|                                                                        |
------------------------------------------------------------------------"
 
);
 
    }
 
    public static void GenerateEmailBody(int accountId, ReservationModel reservation)
    {
        Console.Clear();
        AccountModel account = GenericAccess<AccountModel>.LoadAll().Where(a => a.Id == accountId).ToList().FirstOrDefault();
        MovieModel movie = GenericAccess<MovieModel>.LoadAll().Where(m => m.Id == reservation.MovieId).ToList().FirstOrDefault();
        Console.WriteLine(
$@"
------------------------------------------------------------------------
|Dear {account.FullName},                                                      |
|                                                                        |
|Thank you for your reservation. Here are your reservation details:      |
|                                                                        |
|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~                        |
|Reservation ID: {reservation.Id}                                                       |
|Movie: {movie.Name}                                                    |
|Date: {reservation.PurchaseTime}                                                |
|Seat(s): {string.Join(", ", reservation.Seats)}                                                             |
|Total price: {reservation.TotalPrice }                                                         |
|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~                        |
|Enjoy your movie!                                                       |
|                                                                        |
|                                                                        |
|Best regards,                                                           |
|                                                                        |
|Team                                                                    |
|    ██████╗  ██████╗ ██╗   ██╗███████╗ ██████╗ ██████╗  ██████╗ ██████╗ |
|    ██╔══██╗██╔═══██╗╚██╗ ██╔╝██╔════╝██╔════╝██╔═══██╗██╔═══██╗██╔══██╗|
|    ██████╔╝██║   ██║ ╚████╔╝ ███████╗██║     ██║   ██║██║   ██║██████╔╝|
|    ██╔══██╗██║   ██║  ╚██╔╝  ╚════██║██║     ██║   ██║██║   ██║██╔═══╝ |
|    ██████╔╝╚██████╔╝   ██║   ███████║╚██████╗╚██████╔╝╚██████╔╝██║     |
|    ╚═════╝  ╚═════╝    ╚═╝   ╚══════╝ ╚═════╝ ╚═════╝  ╚═════╝ ╚═╝     |
|                                                                        |
------------------------------------------------------------------------"
 
);
 
    }
 
 
}
