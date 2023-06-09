using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticePortfolio.Models;

namespace PracticePortfolioTests
{
    [TestClass]
    public class TestingMethodsIntegrationTests
    {

        [TestMethod]
        public void Test_Pay_Multiple_Payments_Logged_Correctly()
        {
            IList<int> dailyHoursWorked = new List<int>() { 8, 8, 7, 8, 0 };
            decimal payRate = 15.25M;
            Employee subject = new(payRate);
            subject.AddHours(dailyHoursWorked);
            int totalHours = dailyHoursWorked.Sum(t => t);
            decimal pay = totalHours * payRate;
            string expectedPaymentStatement = $"Employee was paid ${pay}";

            subject.Pay();

            subject.PaymentLogger.Should().ContainInOrder(expectedPaymentStatement);

        }

    }
}
