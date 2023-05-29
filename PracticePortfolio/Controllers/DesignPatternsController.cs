using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Models;
using PracticePortfolio.Models.DTOs;

namespace PracticePortfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesignPatternsController : Controller
    {

        [HttpGet ("singleton")]
        public IActionResult SingletonDemo(int value, int newValue)
        {
            Singleton firstSingleton = Singleton.GetInstance();
            Singleton secondSingleton = Singleton.GetInstance();

            firstSingleton.Value = value;
            secondSingleton.Value = newValue;

            return Ok(new SingletonPair(firstSingleton, secondSingleton));

        }

        [HttpGet ("adapter")]
        public IActionResult AdapterDemo(decimal amount, string cardNumber, string cvv)
        {
            INewPaymentGateway newPaymentGateway = new NewPaymentGateway();
            NewPaymentGatewayAdapter newPaymentGatewayAdapter = new(newPaymentGateway);
            NewPaymentData expectedNewPaymentData = new(amount, cardNumber, cvv);
            string serializedNewPaymentData = newPaymentGatewayAdapter.SerializeNewPaymentData(expectedNewPaymentData);
            string paymentStatement = newPaymentGatewayAdapter.ProcessPayment(serializedNewPaymentData);

            return Ok(paymentStatement);
        }

    }
}
