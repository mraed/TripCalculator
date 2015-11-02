using System;
using System.Collections.Generic;
using TripCalculator.Models;
using NUnit.Framework;
using TripCalculator.BusinessLogic;

namespace TripCalculator.Tests.Unit_Tests
{
    [TestFixture]
    public class TripCalculatorTests
    {
        public ExpenseCalculator expenseCalculator;

        [TestFixtureSetUp]
        public void Setup()
        {
            expenseCalculator = new ExpenseCalculator();
        }

        [Test]
        public void HappyPathValuesInDecimalPlaces()
        {
            var trip = new Trip { Expenses = new List<double> { 15.01, 15, 3, 3.01 } };
            var retval = expenseCalculator.GetAmountOwed(trip);
            Assert.AreEqual(11.99, retval);
        }

        [Test]
        public void HappyPathZeroesInDecimalPlaces()
        {
            var trip = new Trip { Expenses = new List<double> { 10, 20, 30 } };
            var retval = expenseCalculator.GetAmountOwed(trip);
            Assert.AreEqual(10, retval);
        }

        [Test]
        public void NegativeExpenseValueThrowsException()
        {
            var trip = new Trip { Expenses = new List<double> { -15.01, 15, 3, 3.01 } };
            Assert.Throws<ArgumentException>(() => expenseCalculator.GetAmountOwed(trip));
        }
    }
}
