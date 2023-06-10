using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticePortfolio.Models;
using PracticePortfolio.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace PracticePortfolioTests
{
    [TestClass]
    public class TestingMethodsIntegrationTests
    {
        public static IEnumerable<object[]> TestingData()
        {

            yield return new object[]
            {
                "John Doe",
                new List<int>() { 0, 8, 6, 10, 0, 0 },
                new List<int>() { 0, 8, 8, 8, 8, 8, 0 },
                new List<int>() { 0, 8, 12, 8, 10, 8, 0 },
                13.00M
            };
        }

        [TestMethod]
        [DynamicData(nameof(TestingData), DynamicDataSourceType.Method)]
        public void Test_Pay_Multiple_Payments_Logged_Correctly(
            string name, IList<int> dailyHoursWorked1, IList<int> dailyHoursWorked2, IList<int> dailyHoursWorked3, decimal payRate)
        {
            Employee subject = new(name, payRate);
            subject.AddHours(dailyHoursWorked1);
            int totalHours1 = dailyHoursWorked1.Sum(t => t);
            decimal pay1 = totalHours1 * payRate;
            string expectedPaymentStatement1 = $"Employee was paid ${pay1}";

            decimal result1 = subject.Pay();
            result1.Should().Be(pay1);

            subject.AddHours(dailyHoursWorked2);
            int totalHours2 = dailyHoursWorked2.Sum(t => t);
            decimal pay2 = totalHours2 * payRate;
            string expectedPaymentStatement2 = $"Employee was paid ${pay2}";

            decimal result2 = subject.Pay();
            result2.Should().Be(pay2);

            subject.AddHours(dailyHoursWorked3);
            int totalHours3 = dailyHoursWorked3.Sum(t => t);
            decimal pay3 = totalHours3 * payRate;
            string expectedPaymentStatement3 = $"Employee was paid ${pay3}";

            decimal result3 = subject.Pay();
            result3.Should().Be(pay3);

            subject.PaymentLogger.Should().ContainInOrder(expectedPaymentStatement1, expectedPaymentStatement2, expectedPaymentStatement3);
        }

        [TestMethod]
        [DynamicData(nameof(TestingData), DynamicDataSourceType.Method)]
        public void Test_WrapMethodDemo_Returns_Correct_Amount(
            string name, IList<int> dailyHoursWorked1, IList<int> dailyHoursWorked2, IList<int> dailyHoursWorked3, decimal payRate)
        {
            TestingMethodsController subject = new();
            IList<int>[] hoursWorkedTotalList = new IList<int>[] { dailyHoursWorked1, dailyHoursWorked2, dailyHoursWorked3 };
            int totalHoursWorked = dailyHoursWorked1.Sum(t => t) + dailyHoursWorked2.Sum(t => t) + dailyHoursWorked3.Sum(t => t);
            decimal expectedPay = totalHoursWorked * payRate;

            IActionResult result = subject.WrapMethodDemo(name, payRate, hoursWorkedTotalList);
            decimal amount = (decimal)(((OkObjectResult)result).Value ?? 0);

            result.Should().BeOfType<OkObjectResult>();
            amount.Should().Be(expectedPay);
        }
    }
}
