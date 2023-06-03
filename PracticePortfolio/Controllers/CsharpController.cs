using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Models;
using PracticePortfolio.Models.DTOs;

namespace PracticePortfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsharpController : Controller
    {

        [HttpGet("explicit-operator")]
        public IActionResult ExplicitOperatorDemo(double kilograms) => Ok((ImperialPound)kilograms);

    }
}
