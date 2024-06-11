using static SeeJsons;
 
static class EmailConf
{
    public static void GenerateEmailBody(AccountModel account, ReservationModel reservation)
    {
        ConsoleE.Clear();
        MovieModel movie = GenericAccess<MovieModel>.LoadAll().Where(m => m.Id == reservation.Id).ToList().FirstOrDefault();
        Console.WriteLine(
$@"
------------------------------------------------------------------------
|Dear {account.FullName},                                                     
|                                                                        |
|Thank you for your reservation. Here are your reservation details:      |
|                                                                        |
|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 
|Reservation Code: {reservation.ReservationCode}                       
|Reservation ID: {reservation.Id}  
|Movie: {movie.Name}                                                   
|Date: {reservation.PurchaseTime}
|Seat(s): {string.Join(", ", reservation.Seats)}
|Ordered food: {string.Join(", ", reservation.Food)}
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
        ConsoleE.Clear();
        AccountModel account = GenericAccess<AccountModel>.LoadAll().Where(a => a.Id == accountId).ToList().FirstOrDefault();
        MovieModel movie = GenericAccess<MovieModel>.LoadAll().Where(m => m.Id == reservation.MovieId).ToList().FirstOrDefault();
        Console.WriteLine(
$@"
------------------------------------------------------------------------
|Dear {account.FullName},                                                      
|                                                                        |
|Thank you for your reservation. Here are your reservation details:      |
|                                                                        |
|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 
|Reservation Code: {reservation.ReservationCode}                       
|Reservation ID: {reservation.Id}  
|Movie: {movie.Name}                                                   
|Date: {reservation.PurchaseTime}
|Seat(s): {string.Join(", ", reservation.Seats)}
|Ordered food: {string.Join(", ", reservation.Food)}
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
 
    public static void GenerateEmailBodyNoFood(AccountModel account, ReservationModel reservation)
    {
        ConsoleE.Clear();
        MovieModel movie = GenericAccess<MovieModel>.LoadAll().Where(m => m.Id == reservation.Id).ToList().FirstOrDefault();
        Console.WriteLine(
$@"
------------------------------------------------------------------------
|Dear {account.FullName},                                                     
|                                                                        |
|Thank you for your reservation. Here are your reservation details:      |
|                                                                        |
|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 
|Reservation Code: {reservation.ReservationCode}                       
|Reservation ID: {reservation.Id}  
|Movie: {movie.Name}                                                   
|Date: {reservation.PurchaseTime}
|Seat(s): {string.Join(", ", reservation.Seats)}
|Ordered food: {string.Join(", ", reservation.Food)}
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
 
    public static void GenerateEmailBodyNoFood(int accountId, ReservationModel reservation)
    {
        ConsoleE.Clear();
        AccountModel account = GenericAccess<AccountModel>.LoadAll().Where(a => a.Id == accountId).ToList().FirstOrDefault();
        MovieModel movie = GenericAccess<MovieModel>.LoadAll().Where(m => m.Id == reservation.MovieId).ToList().FirstOrDefault();
        Console.WriteLine(
$@"
------------------------------------------------------------------------
|Dear {account.FullName},                                                      
|                                                                        |
|Thank you for your reservation. Here are your reservation details:      |
|                                                                        |
|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 
|Reservation Code: {reservation.ReservationCode}                       
|Reservation ID: {reservation.Id}  
|Movie: {movie.Name}                                                   
|Date: {reservation.PurchaseTime}
|Seat(s): {string.Join(", ", reservation.Seats)}
|Ordered food: {string.Join(", ", reservation.Food)}
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
 

}
