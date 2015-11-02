using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NUnit.Framework;
using TripCalculator.Controllers;
using TripCalculator.Models;

namespace TripCalculator.Tests.Unit_Tests
{
    [TestFixture]
    class CalculatorControllerTests
    {
        private CalculatorController controller;

        [TestFixtureSetUp]
        public void Setup()
        {
            controller = new CalculatorController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test]
        public void HappyPathCalculateValueInDecimalPlaces()
        {
            var tripList = new TripList { Trip = new List<Trip> { new Trip { Expenses = new List<double> { 15.01, 15, 3, 3.01 } } }};
            var response = controller.CalculateTrip(tripList);

            var expectedValue = "11.99";
            string responseValue;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.TryGetContentValue(out responseValue));
            Assert.AreEqual(expectedValue, responseValue);
        }

        [Test]
        public void HappyPathCalculateNoValueInDecimalPlaces()
        {
            var tripList = new TripList { Trip = new List<Trip> { new Trip { Expenses = new List<double> { 10, 20, 30} } } };
            var response = controller.CalculateTrip(tripList);

            var expectedValue = "10.00";
            string responseValue;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.TryGetContentValue(out responseValue));
            Assert.AreEqual(expectedValue, responseValue);
        }

        [Test]
        public void CalculateCantProcessInput()
        {
            var response = controller.CalculateTrip(null);

            string responseValue;
            var expectedValue = "Invalid data given.";
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsTrue(response.TryGetContentValue(out responseValue));
            Assert.AreEqual(expectedValue, responseValue);
        }
    }
}
