using Moq;
using System.Text.Json;
using FluentAssertions;

namespace ProjectTest;

[TestClass]
public class TestDummy
{
    private static Mock<IFileWrapper> fileMock;
    private const string mockFilePath = "mock_reservations.json";

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        // Setup the mock
        fileMock = new Mock<IFileWrapper>();
        // Initial mock file content

        DateTime now = DateTime.Now;
        var initialReservations = new List<ReservationModel>
        {
            new ReservationModel { Id = 1, AccountId=3, SessionId=1, MovieId=7, Seats="row: 0, seats: 0, 0, 0", Food=new[]{"nothing!"}, TotalPrice=0, PurchaseTime=now}
        };

        string initialJson = JsonSerializer.Serialize(initialReservations);
        fileMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(initialJson);
        fileMock.Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()))
            .Callback<string, string>((path, json) => {
                // Overwrite the ReadAllText setup to return the updated json
                fileMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(json);
            });

        // Inject the mock into GenericAccess<ReservationModel>
        // GenericAccessJson<ReservationModel> genericAccessJson = new GenericAccessJson<ReservationModel>();
        GenericAccess<ReservationModel>.FileWrapper = fileMock.Object;
    }

    [TestMethod]
    public void TestDummyReservation()
    {   
        List<ReservationModel> _reservations = GenericAccess<ReservationModel>.LoadAllJson();
        int reservationsCount = _reservations.Count();
        Console.WriteLine("Reservations: " + reservationsCount);

        ReservationModel dummyResv = new ReservationModel(3, 0, 7, "row: 0, seats: 0, 0, 0", new[] {"dumchos", "dum chips", "dummy!'s"}, 0, DateTime.Now);
        _reservations.Add(dummyResv);
        _reservations.Count().Should().NotBe(reservationsCount);
    }
}
