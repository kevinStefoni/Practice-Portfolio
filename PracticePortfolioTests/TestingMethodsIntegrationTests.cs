using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticePortfolio.Controllers;
using Microsoft.AspNetCore.Mvc;
using EmployeeLibrary;
using EmployeeLibrary.EmployeeTypes;
using ApprovalTests;
using ApprovalTests.Reporters;

namespace PracticePortfolioTests
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class TestingMethodsIntegrationTests
    {
        private static readonly TalentAcquisitionCoordinator _talentAcquisitionCoordinator = TalentAcquisitionCoordinator.AssignTalentAcquisitionSpecialist();

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeData), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_NullEmployee_Pay_Returns_0_And_Does_Not_Add_Log(string name, decimal payRate)
        {
            IScheduleSentry nullScheduleSentry = new NullScheduleSentry();
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new NullEmployeeType(), name, payRate, nullScheduleSentry);
            decimal expectedPay = 0.00M;

            decimal actualPay = subject.Pay();
            IList<string> actualPaymentStatements = subject.PaymentLogger;

            actualPaymentStatements.Should().BeEmpty();
            actualPay.Should().Be(expectedPay);
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeWithMultipleSetsOfHours), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_Pay_Multiple_Payments_Logged_Correctly(
            string name, decimal payRate, IList<int>[] totalHoursWorked)
        {
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), name, payRate);
            IList<string> expectedPaymentStatements = new List<string>();

            foreach(IList<int> hoursWorked in totalHoursWorked)
            {
                subject.AddHours(hoursWorked);
                int totalHours = hoursWorked.Sum(t => t);
                decimal pay = totalHours * payRate;
                expectedPaymentStatements.Add($"Employee was paid ${pay}");

                decimal result = subject.Pay();
            }

            Approvals.VerifyAll(subject.PaymentLogger, "Payment_Statement");
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeWithMultipleSetsOfHours), typeof(TestDataSmith), DynamicDataSourceType.Method)]
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
