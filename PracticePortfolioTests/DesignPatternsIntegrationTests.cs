using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Controllers;
using PracticePortfolio.Models.DTOs;

namespace PracticePortfolioTests
{
    public class DesignPatternsIntegrationTests
    {
        [Theory]
        [InlineData(0, 50)]
        [InlineData(1, 50)]
        [InlineData(-1, 50)]
        [InlineData(5000, 50)]
        [InlineData(-5000, 50)]
        [InlineData(-2147483648, 50)]
        [InlineData(2147483647, 50)]
        [InlineData(-2147483647, 50)]
        [InlineData(2147483646, 50)]
        [InlineData(50, 0)]
        [InlineData(50, 1)]
        [InlineData(50, -1)]
        [InlineData(50, 5000)]
        [InlineData(50, -5000)]
        [InlineData(50, -2147483648)]
        [InlineData(50, 2147483647)]
        [InlineData(50, -2147483647)]
        [InlineData(50, 2147483646)]
        public void Singleton_Demo_Return_Same_Value(int value, int newValue)
        {
            DesignPatternsController designPatternsController = new();

            IActionResult result = designPatternsController.SingletonDemo(value, newValue);
            SingletonPair? singletonPair = ((OkObjectResult)result).Value as SingletonPair;

            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(singletonPair);
            Assert.Equal(newValue, singletonPair.FirstInstance.Value);
            Assert.Equal(singletonPair.SecondInstance.Value, singletonPair.FirstInstance.Value);
        }

        [Theory]
        [InlineData("54.31", "1234567812345678", "123")]
        [InlineData("5999", "1234567812345678", "123")]
        [InlineData("0", "1234567812345678", "123")]
        [InlineData("0.00", "1234567812345678", "123")]
        [InlineData("0.01", "1234567812345678", "123")]
        [InlineData("0.2", "1234567812345678", "123")]
        [InlineData("5.0", "1234567812345678", "123")]
        [InlineData("79228162514264337593543950335", "1234567812345678", "123")]
        [InlineData("79228162514264337593543950334.99", "1234567812345678", "123")]
        public void AdapterDemo_Returns_Payment_Statement(string amount, string cardNumber, string cvv)
        {
            decimal amt = 0M;
            try
            {
                amt = Convert.ToDecimal(amount);
            }
            catch
            {
                Assert.Fail("Invalid amount provided to unit test.");
            }

            DesignPatternsController designPatternsController = new();

            IActionResult result = designPatternsController.AdapterDemo(amt, cardNumber, cvv);
            string? paymentStatement = ((OkObjectResult)result).Value?.ToString();

            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(paymentStatement);
            Assert.Equal($"A ${amt:F2} payment was made using a credit card with the credit card number {cardNumber} and a CVV of {cvv} using the new payment gateway.", paymentStatement);

        }

        [Theory]
        [InlineData("-10.25", "1234567812345678", "123")]
        [InlineData("0.001", "1234567812345678", "123")]
        [InlineData("0.0000000000054278", "1234567812345678", "123")]
        public void AdapterDemo_Invalid_Amount_Returns_Error_Message(string amount, string cardNumber, string cvv)
        {
            decimal amt = 0M;
            try
            {
                amt = Convert.ToDecimal(amount);
            }
            catch
            {
                Assert.Fail("Invalid amount provided to unit test.");
            }

            DesignPatternsController designPatternsController = new();

            IActionResult result = designPatternsController.AdapterDemo(amt, cardNumber, cvv);
            string? paymentStatement = ((OkObjectResult)result).Value?.ToString();

            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(paymentStatement);
            Assert.Equal("Invalid payment data.", paymentStatement);

        }

        [Theory]
        [InlineData("54.31", "123456781234567", "123")]
        [InlineData("54.31", "12345678123456777", "123")]
        [InlineData("54.31", "1234567A123456777", "123")]
        [InlineData("54.31", "1234567_123456777", "123")]
        [InlineData("54.31", "12 34567123456777", "123")]
        [InlineData("54.31", "1234-6789-1485-1841", "123")]
        [InlineData("54.31", "", "123")]
        public void AdapterDemo_Invalid_Card_Number_Returns_Error_Message(string amount, string cardNumber, string cvv)
        {
            decimal amt = 0M;
            try
            {
                amt = Convert.ToDecimal(amount);
            }
            catch
            {
                Assert.Fail("Invalid amount provided to unit test.");
            }

            DesignPatternsController designPatternsController = new();

            IActionResult result = designPatternsController.AdapterDemo(amt, cardNumber, cvv);
            string? paymentStatement = ((OkObjectResult)result).Value?.ToString();

            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(paymentStatement);
            Assert.Equal("Invalid payment data.", paymentStatement);

        }

        [Theory]
        [InlineData("54.31", "1234567812345678", "1237")]
        [InlineData("5999", "1234567812345678", "23")]
        [InlineData("0", "1234567812345678", "")]
        [InlineData("0.00", "1234567812345678", "123A3")]
        [InlineData("0.01", "1234567812345678", "ABC")]
        [InlineData("0.2", "1234567812345678", "#13")]
        public void AdapterDemo_Invalid_Cvv_Returns_Error_Message(string amount, string cardNumber, string cvv)
        {
            decimal amt = 0M;
            try
            {
                amt = Convert.ToDecimal(amount);
            }
            catch
            {
                Assert.Fail("Invalid amount provided to unit test.");
            }

            DesignPatternsController designPatternsController = new();

            IActionResult result = designPatternsController.AdapterDemo(amt, cardNumber, cvv);
            string? paymentStatement = ((OkObjectResult)result).Value?.ToString();

            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(paymentStatement);
            Assert.Equal("Invalid payment data.", paymentStatement);

        }
    }
}
