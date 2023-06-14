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
                    _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(),"John Doe", 15.25M),
                    _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), "Jane Smith", 20.50M),
                    _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), "Nicolas Garcia", 19.25M),
                    _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), "Gracie Smith", 17.25M)
                }
            };
        }

        public static IEnumerable<object[]> MultipleSetsOfHours()
        {
            yield return new object[]
            {
                new IList<int>[]
                {
                    new List<int>() { 0, 8, 6, 10, 0, 0 },
                    new List<int>() { 0, 8, 8, 8, 8, 8, 0 },
                    new List<int>() { 0, 8, 12, 8, 10, 8, 0 },
                }
            };
        }

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
        [DynamicData(nameof(MultipleSetsOfHours), DynamicDataSourceType.Method)]
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
        [DynamicData(nameof(MultipleSetsOfHours), DynamicDataSourceType.Method)]
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
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Pay_No_Hours_Worked_Returns_0(string name, decimal payRate)
        {
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), name, payRate);

            decimal pay = subject.CalculatePay();

            pay.Should().Be(0);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
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
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
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
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Calculate_Pay_Clears_Hours_Worked(string name, decimal payRate)
        {
            IList<int> hoursWorkedList = new List<int>() { 8, 8, 7, 8, 0 };
            IScheduleSentry subject = new MockScheduleSentry(hoursWorkedList);
            IEmployee employee = _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), name, payRate, subject);

            employee.CalculatePay();

            subject.HoursWorked.Should().BeEmpty();
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
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
        [DynamicData(nameof(MultipleEmployeesTestData), DynamicDataSourceType.Method)]
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
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
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
        [DynamicData(nameof(MultipleSetsOfHours), DynamicDataSourceType.Method)]
        public void Test_NullEmployee_AddHours(IList<int>[] totalHoursWorked)
        {
            string name = string.Empty;
            decimal payRate = 65.52M;
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new NullEmployeeType(), name, payRate);

            foreach (IList<int> setOfHours in totalHoursWorked)
            {
                subject.AddHours(setOfHours);
            }
            IList<int> actualHoursWorked = Privateer.GetHoursWorkedNullEmployee(subject);

            actualHoursWorked.Should().BeEmpty();
        }

        [TestMethod]
        public void Test_NullEmployee_CalculatePay_Returns_0()
        {
            string name = string.Empty;
            decimal payRate = 50.00M;
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new NullEmployeeType(), name, payRate);
            decimal expectedPay = 0.00M;


            decimal actualPay = subject.CalculatePay();

            actualPay.Should().Be(expectedPay);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_NullEmployee_LogPayment_Does_Nothing(string name, decimal payRate)
        {
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new NullEmployeeType(), name, payRate);
            decimal pay = 0.00M;

            subject.LogPayment(pay);

            subject.PaymentLogger.Should().BeEmpty();
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_NullEmployee_Pay_Returns_0_And_Does_Not_Add_Log(string name, decimal payRate)
        {
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new NullEmployeeType(), name, payRate);
            decimal expectedPay = 0.00M;

            decimal actualPay = subject.Pay();
            IList<string> actualPaymentStatements = subject.PaymentLogger;

            actualPaymentStatements.Should().BeEmpty();
            actualPay.Should().Be(expectedPay);
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
