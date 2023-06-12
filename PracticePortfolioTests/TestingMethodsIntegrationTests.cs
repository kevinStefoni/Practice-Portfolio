using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticePortfolio.Models;
using PracticePortfolio.Controllers;
using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Models.Employee_Models;

namespace PracticePortfolioTests
{
    [TestClass]
    public class TestingMethodsIntegrationTests
    {
        public static IEnumerable<object[]> EmployeeWithMultipleSetsOfHours()
        {

            yield return new object[]
            {
                "John Doe",
                13.00M,
                new IList<int>[]
                {
                    new List<int>() { 0, 8, 6, 10, 0, 0 },
                    new List<int>() { 0, 8, 8, 8, 8, 8, 0 },
                    new List<int>() { 0, 8, 12, 8, 10, 8, 0 },
                }
            };
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeWithMultipleSetsOfHours), DynamicDataSourceType.Method)]
        public void Test_Pay_Multiple_Payments_Logged_Correctly(
            string name, decimal payRate, IList<int>[] totalHoursWorked)
        {
            IEmployee subject = EmployeeFactory.GetFactory().CreateEmployee(name, payRate);
            IList<string> expectedPaymentStatements = new List<string>();

            foreach(IList<int> hoursWorked in totalHoursWorked)
            {
                subject.AddHours(hoursWorked);
                int totalHours = hoursWorked.Sum(t => t);
                decimal pay = totalHours * payRate;
                expectedPaymentStatements.Add($"Employee was paid ${pay}");

                decimal result = subject.Pay();
                result.Should().Be(pay);
            }

            subject.PaymentLogger.Should().ContainInOrder(expectedPaymentStatements);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeWithMultipleSetsOfHours), DynamicDataSourceType.Method)]
        public void Test_WrapMethodDemo_Returns_Correct_Amount(
            string name, decimal payRate, IList<int>[] totalHoursWorkedList)
        {
            TestingMethodsController subject = new();
            int totalHoursWorkedSum = totalHoursWorkedList.SelectMany(hours => hours).Sum();
            decimal expectedPay = totalHoursWorkedSum * payRate;

            IActionResult result = subject.WrapMethodDemo(name, payRate, totalHoursWorkedList);
            decimal amount = (decimal)(((OkObjectResult)result).Value ?? 0);

            result.Should().BeOfType<OkObjectResult>();
            amount.Should().Be(expectedPay);
        }
    }
}
