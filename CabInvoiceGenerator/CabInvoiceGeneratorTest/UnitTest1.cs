using NUnit.Framework;
using CabInvoiceGenerator;

namespace CabInvoiceGeneratorTest
{
    public class Tests
    {
        invoiceGenerator invoicegenerator = null;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenDistanceAndTimeShouldReturnTotalFare()
        {
            invoicegenerator = new invoiceGenerator(RideType.NORMAL);
            double distance = 2.0;
            int time = 5;

            double fare = invoicegenerator.CalculateFare(distance, time);
            double expected = 25;

            Assert.AreEqual(expected, fare);
        }

        [Test]
        public void GivenMultipleRidesShouldReturnInvoiceSummary()
        {
            invoicegenerator = new invoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };

            InvoiceSummary summary = invoicegenerator.CalculateFare(rides);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0);

            Assert.AreEqual(expectedSummary, summary);
        }
        [Test]
        public void GivenInvoiceGenerator_WhenUsingInvoiceSummaryClass_ShouldReturnInvoiceSummary()
        {
            invoicegenerator = new invoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            InvoiceSummary summary = invoicegenerator.CalculateFare(rides);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0, 15);
            Assert.AreEqual(expectedSummary, summary);
        }
        [Test]
        public void GivenUserId_WhenInvoivceService_ShouldReturnInvoice()
        {
            invoicegenerator = new invoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            invoicegenerator.AddRides("101", rides);
            InvoiceSummary summary = invoicegenerator.GetInvoiceSummary("101");
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0, "101");
            Assert.AreEqual(expectedSummary, summary);
        }
        [Test]
        public void GivenRides_ForPremiumUser_ShouldReturnTotalFare()
        {
            invoicegenerator = new invoiceGenerator(RideType.PREMIUM);
            double distance = 3.0;
            int time = 10;
            double fare = invoicegenerator.CalculateFare(distance, time);
            double expected = 65;
            Assert.AreEqual(expected, fare);
        }
    }
}