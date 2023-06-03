using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Controllers;
using PracticePortfolio.Models;


namespace PracticePortfolioTests
{
    public class OperationsIntegrationTests
    {

        [Fact]
        public void ExplicitOperatorDemo_Returns_ImperialPound_And_Converts_Kilogram_To_Imperial_Pound()
        {
            double kilograms = 3.245;
            double expectedImperialWeight = kilograms * 2.20462;
            CsharpController csharpController = new();

            IActionResult result = csharpController.ExplicitOperatorDemo(kilograms);
            ImperialPound? subject = (ImperialPound) (((OkObjectResult)result).Value ?? 0);

            subject.Should().NotBeNull();
            subject.Should().BeOfType<ImperialPound>();
            subject?.Pounds.Should().Be(expectedImperialWeight);

        }


    }
}
