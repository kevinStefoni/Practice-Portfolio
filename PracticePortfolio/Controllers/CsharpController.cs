using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Models;
using PracticePortfolio.Models.DTOs;
using PracticePortfolio.Models.Entities;

namespace PracticePortfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsharpController : Controller
    {

        [HttpGet("explicit-operator")]
        public IActionResult ExplicitOperatorDemo(double kilograms) => Ok((ImperialPound)kilograms);

        [HttpGet("implicit-operator")]
        public IActionResult ImplicitOperatorDemo()
        {
            UserDTO userDTO = new User()
            {
                Id = 123,
                Name = "John Doe",
                Email = "jdoe@gmail.com",
                Password = "tundrasprinkleracidicyellow"
            };

            return Ok(userDTO);
        }

    }
}
