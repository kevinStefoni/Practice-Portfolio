using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticePortfolio.Models;
using System.Reflection;

namespace PracticePortfolioTests
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class TestingMethodsUnitTests
    {
        public static IEnumerable<object[]> EmployeeTestData()
        {
            yield return new object[] { "John Doe", 15.25M };
        }

        public static IEnumerable<object[]> MultipleEmployeesTestData()
        {
            yield return new object[]
            {
                new IEmployee[]
                {
                    new Employee("John Doe", 15.25M),
                    new Employee("Jane Smith", 20.50M),
                    new Employee("Nicolas Garcia", 19.25M),
                    new Employee("Gracie Smith", 17.25M)
                }
            };
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Pay_No_Hours_Worked_Returns_0(string name, decimal payRate)
        {
            Employee subject = new(name, payRate);

            decimal pay = subject.CalculatePay();

            pay.Should().Be(0);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Pay_1_Hour_Worked_In_One_Day_Returns_Pay_Rate(string name, decimal payRate)
        {
            IList<int> dailyHoursWorked = new List<int>() { 1 };
            Employee subject = new(name, payRate);
            subject.AddHours(dailyHoursWorked);
            decimal expectedPay = payRate;

            decimal pay = subject.CalculatePay();

            pay.Should().Be(expectedPay);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Pay_Hours_Worked_In_One_Day_Returns_Correct_Pay(string name, decimal payRate)
        {
            int hoursInOneDay = 5;
            IList<int> dailyHoursWorked = new List<int>() { hoursInOneDay };
            Employee subject = new(name, payRate);
            subject.AddHours(dailyHoursWorked);
            decimal expectedPay = hoursInOneDay * payRate;

            decimal pay = subject.CalculatePay();

            pay.Should().Be(expectedPay);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Pay_Hours_Worked_In_Multiple_Days_Returns_Correct_Pay(string name, decimal payRate)
        {
            IList<int> dailyHoursWorked = new List<int>() { 8, 8, 7, 8, 0 };
            Employee subject = new(name, payRate);
            subject.AddHours(dailyHoursWorked);
            int totalHours = dailyHoursWorked.Sum(t => t);
            decimal expectedPay = totalHours * payRate;

            decimal pay = subject.CalculatePay();

            pay.Should().Be(expectedPay);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Log_Payment_Returns_Payment_Confirmation_Statement_With_Correct_Amount(string name, decimal payRate)
        {
            IList<int> dailyHoursWorked = new List<int>() { 8, 8, 7, 8, 0 };
            IEmployee subject = new Employee(name, payRate);
            subject.AddHours(dailyHoursWorked);
            int totalHours = dailyHoursWorked.Sum(t => t);
            decimal pay = totalHours * payRate;
            string expectedPaymentStatement = $"Employee was paid ${pay}";

            subject.LogPayment(pay);
            IList<string> payments = subject.PaymentLogger;

            payments.Should().OnlyContain(actualStatement => actualStatement == expectedPaymentStatement);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Add_Multiple_Weeks_Of_Hours(string name, decimal payRate)
        {
            IList<int> hoursWorkedLastWeek1 = new List<int>() { 0, 8, 8, 7, 4, 8, 0 };
            IList<int> hoursWorkedLastWeek2 = new List<int>() { 0, 8, 5, 7, 10, 8, 0 };
            IList<int> expectedHoursWorked = hoursWorkedLastWeek1.Concat(hoursWorkedLastWeek2).ToList();
            IEmployee subject = new Employee(name, payRate);

            subject.AddHours(hoursWorkedLastWeek1);
            subject.AddHours(hoursWorkedLastWeek2);
            IList<int> payments = Privateer.GetHoursWorked(subject);

            payments.Should().BeEquivalentTo(expectedHoursWorked);
        }

        [TestMethod]
        [DynamicData(nameof(MultipleEmployeesTestData), DynamicDataSourceType.Method)]
        public void Test_To_String_Returns_Employee_Name_And_Pay_Rate(IEmployee[] employees)
        {
            IEmployee[] subject = employees;
            string result = string.Empty;

            foreach(IEmployee emp in subject)
            {
                result += emp.ToString() + "\r\n";
            }

            Approvals.Verify(result);

        }


        public class Privateer
        {
            public static List<int> GetHoursWorked(IEmployee employee)
            {
                var fieldInfo = typeof(Employee).GetField("_hoursWorked", BindingFlags.NonPublic | BindingFlags.Instance);
                return (List<int>)(fieldInfo?.GetValue(employee) ?? new List<int>());
            }
        }
    }
}
