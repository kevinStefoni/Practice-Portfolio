using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Controllers;
using PracticePortfolio.Models;

namespace PracticePortfolioTests
{
    public class OperationsUnitTests
    {
        [Fact]
        public void Create_And_Set_Value_Of_ImperialPounds_Object()
        {
            ImperialPound subject = new(5.5259);
            subject.Should().BeOfType<ImperialPound>();
            subject.Pounds.Should().Be(5.5259);
        }

        [Fact]
        public void Convert_Kilogram_To_ImperialPound()
        {
            double kilograms = 3.245;
            double expectedImperialWeight = kilograms * 2.20462;

            ImperialPound subject = (ImperialPound) kilograms;

            subject.Pounds.Should().Be(expectedImperialWeight);

        }

        [Fact]
        public void ExplicitOperatorDemo_Create_Object()
        {
            CsharpController subject = new();

            subject.Should().BeOfType<CsharpController>();

        }

        [Fact]
        public void ExplicitOperatorDemo_Returns_OkObjectResult()
        {
            CsharpController csharpController = new();

            IActionResult subject = csharpController.ExplicitOperatorDemo(0);

            subject.Should().BeOfType<OkObjectResult>();

        }

    }
}
