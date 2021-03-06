using NUnit.Framework;
using CabInvoiceGeneratorProgram;
using System.Collections.Generic;
namespace CabInvoiceGeneratorTest
{
    public class Tests
    {
        InvoiceGenerator invoiceGenerator;
        List<Ride> rideList;

        [SetUp]
        public void Setup()
        {
            invoiceGenerator = new InvoiceGenerator();
        }
        [Test]
        public void GivenDistanceAndTimeForNormalRide_Should_Return_Fare()
        {
            double distance = 5; //in km
            int time = 20;   //in minutes
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            double fare = invoiceGenerator.CalculateFare(new Ride(distance, time));

            Assert.AreEqual(70, fare);
        }
        [Test]
        public void GivenDistanceAndTimeForPremiumRide_Should_Return_Fare()
        {
            double distance = 5; //in km
            int time = 20;   //in minutes
            invoiceGenerator = new InvoiceGenerator(RideType.PREMIUM);

            double fare = invoiceGenerator.CalculateFare(new Ride(distance, time));

            Assert.AreEqual(115, fare);
        }

        [Test]
        public void GivenInvalidDistance_Should_Return_CabInvoiceException()
        {
            double distance = -5; //in km
            int time = 20;   //in minute
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.CalculateFare(new Ride(distance, time)));

            Assert.AreEqual(CabInvoiceException.Type.INVALID_DISTANCE, exception.type);
        }
        [Test]
        public void GivenInvalidTime_Should_Return_CabInvoiceException()
        {
            double distance = 5; //in km
            int time = -20;   //in minute
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.CalculateFare(new Ride(distance, time)));

            Assert.AreEqual(CabInvoiceException.Type.INVALID_TIME, exception.type);
        }
        [Test]
        public void GivenListOfRides_Should_Return_TotalFare()
        {
            rideList = new List<Ride> { new Ride(5, 20), new Ride(3, 15), new Ride(2, 10) };
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            double fare = invoiceGenerator.CalculateFareForMultipleRides(rideList);

            Assert.AreEqual(145, fare);

        }
        [Test]
        public void GivenNullRides_Should_Return_CabInvoiceException()
        {
            rideList = new List<Ride> { new Ride(5, 20), null, new Ride(2, 10) };
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.CalculateFareForMultipleRides(rideList));

            Assert.AreEqual(CabInvoiceException.Type.NULL_RIDES, exception.type);
        }
        [Test]
        public void GivenListOfRides_Should_Return_InvoiceData()
        {
            rideList = new List<Ride> { new Ride(5, 20), new Ride(3, 15), new Ride(2, 10) };
            double expectedFare = 145;
            int expectedRides = 3;
            double expectedAverage = expectedFare / expectedRides;
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            InvoiceData data = invoiceGenerator.GetInvoiceSummary(rideList);

            Assert.IsTrue(data.noOfRides == expectedRides && data.totalFare == expectedFare && data.averageFare == expectedAverage);
        }
        [Test]
        public void GivenNullRides_WhenAddingToDictionary_Should_Return_CabInvoiceException()
        {
            rideList = new List<Ride> { new Ride(5, 20), null, new Ride(2, 10) };
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.AddRides(1, rideList));

            Assert.AreEqual(CabInvoiceException.Type.NULL_RIDES, exception.type);

        }
        [Test]
        public void GivenUserId_WhenPresent_Should_Return_CabInvoiceSummary()
        {
            rideList = new List<Ride> { new Ride(5, 20), new Ride(3, 15), new Ride(2, 10) };
            double expectedFare = 145;
            int expectedRides = 3;
            double expectedAverage = expectedFare / expectedRides;
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            invoiceGenerator.AddRides(1, rideList);
            InvoiceData data = invoiceGenerator.GetUserInvoice(1);

            Assert.IsTrue(data.noOfRides == expectedRides && data.totalFare == expectedFare && data.averageFare == expectedAverage);
        }
        [Test]
        public void GivenUserId_WhenAbsent_Should_Return_CabInvoiceException()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.GetUserInvoice(1));

            Assert.AreEqual(CabInvoiceException.Type.INVALID_USER_ID, exception.type);
        }

    }
}