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
        public void GivenDistanceAndTime_Should_Return_Fare()
        {
            double distance = 5; //in km
            int time = 20;   //in minutes

            double fare = invoiceGenerator.CalculateFare(new Ride(distance, time));

            Assert.AreEqual(70, fare);
        }

        [Test]
        public void GivenInvalidDistance_Should_Return_CabInvoiceException()
        {
            double distance = -5; //in km
            int time = 20;   //in minute

            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.CalculateFare(new Ride(distance, time)));

            Assert.AreEqual(CabInvoiceException.Type.INVALID_DISTANCE, exception.type);
        }
        [Test]
        public void GivenInvalidTime_Should_Return_CabInvoiceException()
        {
            double distance = 5; //in km
            int time = -20;   //in minute

            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.CalculateFare(new Ride(distance, time)));

            Assert.AreEqual(CabInvoiceException.Type.INVALID_TIME, exception.type);
        }
        [Test]
        public void GivenListOfRides_Should_Return_TotalFare()
        {
            rideList = new List<Ride> { new Ride(5, 20), new Ride(3, 15), new Ride(2, 10) };

            double fare = invoiceGenerator.CalculateFareForMultipleRides(rideList);

            Assert.AreEqual(145, fare);

        }
        [Test]
        public void GivenNullRides_Should_Return_CabInvoiceException()
        {
            rideList = new List<Ride> { new Ride(5, 20), null, new Ride(2, 10) };

            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.CalculateFareForMultipleRides(rideList));

            Assert.AreEqual(CabInvoiceException.Type.NULL_RIDES, exception.type);
        }
    }
}