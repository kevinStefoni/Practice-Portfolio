using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeLibrary;
using System.Reflection;
using EmployeeLibrary.EmployeeTypes;
using System.Xml.Linq;
using PracticePortfolioTests.Mocks;

namespace PracticePortfolioTests
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class TestingMethodsUnitTests
    {
        private static readonly TalentAcquisitionCoordinator _talentAcquisitionCoordinator = TalentAcquisitionCoordinator.AssignTalentAcquisitionSpecialist();

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.MultipleSetsOfHours), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_Schedule_Sentry_Add_Multiple_Weeks_Of_Hours(IList<int>[] totalHoursWorkedList)
        {
            IScheduleSentry subject = new ScheduleSentry();
            IList<int> expectedHoursWorkedList = new List<int>();
            foreach (IList<int> hoursWorkedList in totalHoursWorkedList)
            {
                expectedHoursWorkedList = expectedHoursWorkedList.Concat(hoursWorkedList).ToList();
            }

            foreach (IList<int> hoursWorkedList in totalHoursWorkedList)
            {
                subject.AddHours(hoursWorkedList);
            }

            IList<int> actualHoursWorkedList = subject.HoursWorked;

            actualHoursWorkedList.Should().BeEquivalentTo(expectedHoursWorkedList);
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.MultipleSetsOfHours), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_Schedule_Sentry_Clear_Hours(IList<int>[] totalHoursWorkedList)
        {
            IList<int> hoursWorkedList = totalHoursWorkedList.SelectMany(t => t).ToList();
            if (hoursWorkedList.Count < 0)
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail();
            IScheduleSentry scheduleSentry = new ScheduleSentry();
            scheduleSentry.AddHours(hoursWorkedList);

            scheduleSentry.ClearHours();

            scheduleSentry.HoursWorked.Should().BeEmpty();
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeData), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_Pay_No_Hours_Worked_Returns_0(string name, decimal payRate)
        {
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), name, payRate);

            decimal pay = subject.CalculatePay();

            pay.Should().Be(0);
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeData), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_Pay_1_Hour_Worked_In_One_Day_Returns_Pay_Rate(string name, decimal payRate)
        {
            IList<int> hoursWorkedList = new List<int>() { 1 };
            IScheduleSentry mockScheduleSentry = new MockScheduleSentry(hoursWorkedList);
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), name, payRate, mockScheduleSentry);
            decimal expectedPay = payRate;

            decimal pay = subject.CalculatePay();

            pay.Should().Be(expectedPay);
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeData), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_Pay_Hours_Worked_In_Multiple_Days_Returns_Correct_Pay(string name, decimal payRate)
        {
            IList<int> hoursWorkedList = new List<int>() { 8, 8, 7, 8, 0 };
            IScheduleSentry mockScheduleSentry = new MockScheduleSentry(hoursWorkedList);
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), name, payRate, mockScheduleSentry);

            int totalHours = hoursWorkedList.Sum(t => t);
            decimal expectedPay = totalHours * payRate;

            decimal pay = subject.CalculatePay();

            pay.Should().Be(expectedPay);
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeData), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_Calculate_Pay_Clears_Hours_Worked(string name, decimal payRate)
        {
            IList<int> hoursWorkedList = new List<int>() { 8, 8, 7, 8, 0 };
            IScheduleSentry subject = new MockScheduleSentry(hoursWorkedList);
            IEmployee employee = _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), name, payRate, subject);

            employee.CalculatePay();

            subject.HoursWorked.Should().BeEmpty();
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeData), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_Log_Payment_Returns_Payment_Confirmation_Statement_With_Correct_Amount(string name, decimal payRate)
        {
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), name, payRate);

            int totalHours = 40;
            decimal pay = totalHours * payRate;
            string expectedPaymentStatement = $"Employee was paid ${pay}";

            subject.LogPayment(pay);
            IList<string> payments = subject.PaymentLogger;

            payments.Should().OnlyContain(actualStatement => actualStatement == expectedPaymentStatement);
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.MultipleEmployeesTestData), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_To_String_Returns_Employee_Name_And_Pay_Rate(IEmployee[] employees)
        {
            IEmployee[] subject = employees;

            Approvals.VerifyAll(subject, "Employee_Statement");
        }

        [TestMethod]
        public void Test_TalentAcquisitionCoordinator_Returns_ConcreteTalentAcquisitionCoordinator()
        {
            TalentAcquisitionCoordinator subject = _talentAcquisitionCoordinator;

            subject.Should().BeOfType<TalentAcquisitionSpecialist>();
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeData), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_TalentAcquisitionCoordinator_When_EmployeeType_Provided_Returns_Employee(string name, decimal payRate)
        {
            TalentAcquisitionCoordinator subject = _talentAcquisitionCoordinator;
            IEmployee employee = subject.CreateEmployee(new EmployeeType(), name, payRate);
            decimal actualPayRate = Privateer.GetPayRateEmployee(employee);
            
            employee.Should().BeOfType<Employee>();
            employee.Name.Should().Be(name);
            actualPayRate.Should().Be(payRate);
        }

        [TestMethod]
        public void Test_TalentAcquisitionCoordinator_When_NullEmployeeType_Provided_Returns_NullEmployee()
        {
            string name = string.Empty;
            decimal payRate = 65.52M;
            decimal expectedPayRate = 0.00M;

            TalentAcquisitionCoordinator subject = _talentAcquisitionCoordinator;
            IEmployee employee = subject.CreateEmployee(new NullEmployeeType(), name, payRate);
            decimal actualPayRate = Privateer.GetPayRateNullEmployee(employee);

            employee.Should().BeOfType<NullEmployee>();
            employee.Name.Should().Be(name);
            actualPayRate.Should().Be(expectedPayRate);
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeWithMultipleSetsOfHours), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_NullEmployee_AddHours_Should_Always_Return_Empty(string name, decimal payRate, IList<int>[] totalHoursWorked)
        {
            IScheduleSentry nullScheduleSentry = new NullScheduleSentry();
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new NullEmployeeType(), name, payRate, nullScheduleSentry);
            foreach (IList<int> setOfHours in totalHoursWorked)
            {
                subject.AddHours(setOfHours);
            }

            nullScheduleSentry.HoursWorked.Should().BeEmpty();
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeWithMultipleSetsOfHours), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_NullScheduleSentry_AddHours_Should_Always_Return_Empty(string name, decimal payRate, IList<int>[] totalHoursWorked)
        {
            IScheduleSentry subject = new NullScheduleSentry();

            foreach (IList<int> setOfHours in totalHoursWorked)
            {
                subject.AddHours(setOfHours);
            }

            subject.HoursWorked.Should().BeEmpty();
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeWithOneSetOfHours), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_NullEmployee_CalculatePay_Returns_0(string name, decimal payRate, IList<int> hoursWorkedList)
        {
            IScheduleSentry mockScheduleSentry = new MockScheduleSentry(hoursWorkedList);
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new NullEmployeeType(), name, payRate, mockScheduleSentry);
            decimal expectedPay = 0.00M;

            decimal actualPay = subject.CalculatePay();

            actualPay.Should().Be(expectedPay);
        }

        [TestMethod]
        [DynamicData(nameof(TestDataSmith.EmployeeData), typeof(TestDataSmith), DynamicDataSourceType.Method)]
        public void Test_NullEmployee_LogPayment_Does_Not_Add_Statement_To_PaymentLogger(string name, decimal payRate)
        {
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new NullEmployeeType(), name, payRate);
            decimal pay = 50.00M;

            subject.LogPayment(pay);

            subject.PaymentLogger.Should().BeEmpty();
        }


        public class Privateer
        {
            public static List<int> GetHoursWorkedEmployee(IEmployee employee)
            {
                FieldInfo? fieldInfo = typeof(Employee).GetField("_hoursWorked", BindingFlags.NonPublic | BindingFlags.Instance);
                return (List<int>)(fieldInfo?.GetValue(employee) ?? new List<int>());
            }

            public static List<int> GetHoursWorkedNullEmployee(IEmployee employee)
            {
                FieldInfo? fieldInfo = typeof(NullEmployee).GetField("_hoursWorked", BindingFlags.NonPublic | BindingFlags.Instance);
                return (List<int>)(fieldInfo?.GetValue(employee) ?? new List<int>());
            }

            public static decimal GetPayRateEmployee(IEmployee employee)
            {
                FieldInfo? fieldInfo = typeof(Employee).GetField("_payRate", BindingFlags.NonPublic | BindingFlags.Instance);
                return (decimal) (fieldInfo?.GetValue(employee) ?? 0.00M);
            }

            public static decimal GetPayRateNullEmployee(IEmployee employee)
            {
                FieldInfo? fieldInfo = typeof(NullEmployee).GetField("_payRate", BindingFlags.NonPublic | BindingFlags.Instance);
                return (decimal)(fieldInfo?.GetValue(employee) ?? 0.00M);
            }

        }
    }
}
