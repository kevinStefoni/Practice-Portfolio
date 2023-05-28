using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Models;

namespace PracticePortfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesignPatternsController : Controller
    {

        [HttpGet ("singleton")]
        public IActionResult SingletonDemo(int value, int newValue)
        {
            Singleton_ firstSingleton = Singleton_.GetInstance();
            Singleton_ secondSingleton = Singleton_.GetInstance();

            firstSingleton.Value = value;
            secondSingleton.Value = newValue;

            return Ok(new SingletonPair(firstSingleton, secondSingleton));

        }

    }
}
