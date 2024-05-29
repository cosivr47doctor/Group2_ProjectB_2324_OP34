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
            new ReservationModel {Id=1,AccountId=3,SessionId=1,MovieId=7,Seats="row: 100, seats: 100, 100, 100",Food=new[]{"a lot"},TotalPrice=0,PurchaseTime=now},
            new ReservationModel { Id=2, AccountId=3, SessionId=10, MovieId=7, Seats="row: 0, seats: 0, 0, 0", Food=new[]{"nothing!"}, TotalPrice=0, PurchaseTime=now}
        };

        string initialJson = JsonSerializer.Serialize(initialReservations); //JsonSerializer.Serialize(new List<ReservationModel>());
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
        /*NOTE: No need to load _reservations more than once since it and the content are each reference types.
         The DeepClone() method was originally to make sure that `_reservations.Last()` wouldn't be changed to dummyResv, but apparently
            wasn't neccessary as the Last() method already makes a deepcopy? ReservationModel doesn't implement ICloneable.
        */
        List<ReservationModel> _reservations = GenericAccess<ReservationModel>.LoadAllJson();
        int reservationsCount = _reservations.Count();
        reservationsCount.Should().Be(2);
        Console.WriteLine(reservationsCount);

        ReservationModel resvClone = _reservations.Last()/*.DeepClone()*/;

        ReservationModel dummyResv = new ReservationModel(3, 0, 7, "row: 0, seats: 0, 0, 0", new[] {"dumchos", "dum chips", "dummy!'s"}, 0, DateTime.Now)
        {Id = 1 + resvClone.Id};
        _reservations.Add(dummyResv);

        _reservations.Count().Should().NotBe(reservationsCount);
        resvClone.SessionId.Should().Be(10);
        dummyResv.SessionId.Should().Be(0);

        _reservations.Last().Id.Should().Be(3);
        _reservations.Last().Id.Should().Be(_reservations.Count());
    }
}
