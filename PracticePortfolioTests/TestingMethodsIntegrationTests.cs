using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticePortfolio.Models;
using PracticePortfolio.Controllers;
using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Models.DTOs;

namespace PracticePortfolioTests
{
    [TestClass]
    public class TestingMethodsIntegrationTests
    {

        [TestMethod]
        public void Test_Pay_Multiple_Payments_Logged_Correctly()
        {
            IList<int> dailyHoursWorked1 = new List<int>() { 0, 8, 8, 7, 8, 0 };
            IList<int> dailyHoursWorked2 = new List<int>() { 0, 8, 12, 7, 7, 0 };
            decimal payRate = 15.25M;
            Employee subject = new(payRate);
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

            subject.PaymentLogger.Should().ContainInOrder(expectedPaymentStatement1, expectedPaymentStatement2);

        }

        [TestMethod]
        public void Test_WrapMethodDemo_Returns_Correct_Amount()
        {
            TestingMethodsController subject = new();
            decimal payRate = 13.00M;
            IList<int> dailyHoursWorked1 = new List<int>() { 0, 8, 6, 10, 0, 0 };
            IList<int> dailyHoursWorked2 = new List<int>() { 0, 8, 8, 8, 8, 8, 0};
            IList<int> dailyHoursWorked3 = new List<int>() { 0, 8, 12, 8, 10, 8, 0};
            IList<int>[] hoursWorkedTotalList = new IList<int>[] {dailyHoursWorked1, dailyHoursWorked2, dailyHoursWorked3 };
            int totalHoursWorked = dailyHoursWorked1.Sum(t => t) + dailyHoursWorked2.Sum(t => t) + dailyHoursWorked3.Sum(t => t);
            decimal expectedPay = totalHoursWorked * payRate;

            IActionResult result = subject.WrapMethodDemo(payRate, hoursWorkedTotalList);
            decimal amount = (decimal)(((OkObjectResult)result).Value ?? 0);

            result.Should().BeOfType<OkObjectResult>();
            amount.Should().Be(expectedPay);

        }

        [TestMethod]
        public void Test_WrapMethodDemoWithPayload_Returns_Correct_Amount()
        {
            TestingMethodsController subject = new();
            decimal payRate = 13.00M;
            IList<int> dailyHoursWorked1 = new List<int>() { 0, 8, 6, 10, 0, 0 };
            IList<int> dailyHoursWorked2 = new List<int>() { 0, 8, 8, 8, 8, 8, 0 };
            IList<int> dailyHoursWorked3 = new List<int>() { 0, 8, 12, 8, 10, 8, 0 };
            IList<int>[] hoursWorkedTotalList = new IList<int>[] { dailyHoursWorked1, dailyHoursWorked2, dailyHoursWorked3 };
            int totalHoursWorked = dailyHoursWorked1.Sum(t => t) + dailyHoursWorked2.Sum(t => t) + dailyHoursWorked3.Sum(t => t);
            decimal expectedPay = totalHoursWorked * payRate;
            WrapMethodPayload payload = new() { PayRate = payRate, DailyHoursWorked = hoursWorkedTotalList };

            IActionResult result = subject.WrapMethodDemoWithPayload(payload);
            decimal amount = (decimal)(((OkObjectResult)result).Value ?? 0);

            result.Should().BeOfType<OkObjectResult>();
            amount.Should().Be(expectedPay);
        }

    }
}
