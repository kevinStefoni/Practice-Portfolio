using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Models;
using PracticePortfolio.Models.DTOs;

namespace PracticePortfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestingMethodsController : Controller
    {

        [HttpPost("wrap-method-payload")]
        public IActionResult WrapMethodDemoWithPayload([FromBody] WrapMethodPayload payload)
        {
            decimal payRate = payload.PayRate;
            IList<int>[] dailyHoursWorked = payload.DailyHoursWorked;

            return WrapMethodDemo(payRate, dailyHoursWorked);
        }

        public IActionResult WrapMethodDemo(decimal payRate, params IList<int>[] dailyHoursWorked)
        {
            Employee employee = new(payRate);
            foreach (IList<int> hours in dailyHoursWorked)
            {
                employee.AddHours(hours);
            }

            /*
             * NOTE: CalculatePay() used to be Pay(), but client doesn't know because it has same signature and behavior, 
             * even if logger functionality was added.
             */
            decimal amount = employee.Pay();

            return Ok(amount);
        }
    }
}
