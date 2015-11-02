using System;
using System.Linq;
using TripCalculator.Models;

namespace TripCalculator.BusinessLogic
{
    public class ExpenseCalculator
    {
        public double GetAmountOwed(Trip trip)
        {
            var expenses = trip.Expenses;

            if (expenses.Any(x => x < 0))
            {
                throw new ArgumentException("At least one of the expense values is invalid.");
            }
            var tripCost = expenses.Sum();
            var costPerPayer =  tripCost / expenses.Count;

            double owedTotal = 0;

            foreach(var expense in expenses)
            {
                var difference = costPerPayer - expense;

                if(difference > 0)
                {
                    owedTotal = owedTotal + difference;
                }
            }

            return Math.Truncate(owedTotal * 100) / 100;
        }
    }
}