using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TripCalculator.BusinessLogic;

namespace TripCalculator.Controllers
{
    public class CalculatorController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage CalculateTrip([FromBody] TripList enteredTrip)
        {
            try
            {
                //Json.NET deserializes the given JSON into a list of trips but we only get one at a time
                var trip = enteredTrip.Trip.FirstOrDefault();
                var expenseCalculator = new ExpenseCalculator();
                var amountOwed = string.Format("{0:f2}", expenseCalculator.GetAmountOwed(trip));
                return Request.CreateResponse(HttpStatusCode.OK, amountOwed);
            }
            catch(Exception e)
            {
                var errorMessage = enteredTrip == null ? "Invalid data given." : e.Message;
                var errorResponse = Request.CreateResponse(HttpStatusCode.BadRequest, errorMessage);
                return errorResponse;
            }
        }
    }
}
