using System;
using System.Collections.Generic;
using System.Linq;

namespace TripCalculator.Models
{
    public class Trip
    {
        private List<double> _expenses;

        public List<double> Expenses
        {
            get { return _expenses; }

            set { _expenses = value.Select(ConvertToCurrency).ToList(); }
        }

         //Getting double to act more like actual money with truncation
        private static double ConvertToCurrency(double value)
        {
            return Math.Truncate(value * 100) / 100;
        }
    }
}