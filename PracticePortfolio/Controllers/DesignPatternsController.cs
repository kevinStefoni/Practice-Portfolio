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
            Singleton firstSingleton = Singleton.GetInstance();
            Singleton secondSingleton = Singleton.GetInstance();

            firstSingleton.Value = value;
            secondSingleton.Value = newValue;

            return Ok(new SingletonPair(firstSingleton, secondSingleton));

        }

    }
}
