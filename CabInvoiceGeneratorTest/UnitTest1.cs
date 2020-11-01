using NUnit.Framework;
using CabInvoiceGeneratorProgram;
namespace CabInvoiceGeneratorTest
{
    public class Tests
    {
        InvoiceGenerator invoiceGenerator;

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

    }
}