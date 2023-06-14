using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeLibrary;
using System.Reflection;
using EmployeeLibrary.EmployeeTypes;
using System.Xml.Linq;

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
        [DynamicData(nameof(EmployeeWithMultipleSetsOfHours), DynamicDataSourceType.Method)]
        public void Test_Add_Multiple_Weeks_Of_Hours(string name, decimal payRate, IList<int>[] totalHoursWorkedList)
        {
            IEmployee subject = new TestEmployee(name, payRate);
            IList<int> expectedHoursWorked = new List<int>();
            foreach (IList<int> hoursWorkedList in totalHoursWorkedList)
            {
                expectedHoursWorked = expectedHoursWorked.Concat(hoursWorkedList).ToList();
            }

            foreach (IList<int> hoursWorkedList in totalHoursWorkedList)
            {
                subject.AddHours(hoursWorkedList);
            }
            IList<int> payments = Privateer.GetHoursWorkedEmployee(subject);

            payments.Should().BeEquivalentTo(expectedHoursWorked);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Pay_No_Hours_Worked_Returns_0(string name, decimal payRate)
        {
            IEmployee subject = new TestEmployee(name, payRate);

            decimal pay = subject.CalculatePay();

            pay.Should().Be(0);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Pay_1_Hour_Worked_In_One_Day_Returns_Pay_Rate(string name, decimal payRate)
        {
            IList<int> hoursWorkedList = new List<int>() { 1 };
            IEmployee subject = new TestEmployee(name, payRate)
            {
                HoursWorked = hoursWorkedList
            };
            decimal expectedPay = payRate;

            decimal pay = subject.CalculatePay();

            pay.Should().Be(expectedPay);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Pay_Hours_Worked_In_Multiple_Days_Returns_Correct_Pay(string name, decimal payRate)
        {
            IList<int> hoursWorkedList = new List<int>() { 8, 8, 7, 8, 0 };
            IEmployee subject = new TestEmployee(name, payRate)
            {
                HoursWorked = hoursWorkedList
            };
            int totalHours = hoursWorkedList.Sum(t => t);
            decimal expectedPay = totalHours * payRate;

            decimal pay = subject.CalculatePay();

            pay.Should().Be(expectedPay);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_Log_Payment_Returns_Payment_Confirmation_Statement_With_Correct_Amount(string name, decimal payRate)
        {
            IList<int> hoursWorkedList = new List<int>() { 8, 8, 7, 8, 0 };
            IEmployee subject = new TestEmployee(name, payRate)
            {
                HoursWorked = hoursWorkedList
            };
            int totalHours = hoursWorkedList.Sum(t => t);
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
            IEmployee employee = _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), name, payRate);
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

            IEmployee employee = _talentAcquisitionCoordinator.CreateEmployee(new NullEmployeeType(), name, payRate);
            decimal actualPayRate = Privateer.GetPayRateNullEmployee(employee);

            employee.Should().BeOfType<NullEmployee>();
            employee.Name.Should().Be(name);
            actualPayRate.Should().Be(expectedPayRate);
        }

        [TestMethod]
        [DynamicData(nameof(EmployeeTestData), DynamicDataSourceType.Method)]
        public void Test_TalentAcquisitionCoordinator_When_TestEmployeeType_Provided_Returns_TestEmployee(string name, decimal payRate)
        {
            decimal expectedPayRate = 0.00M;

            IEmployee employee = _talentAcquisitionCoordinator.CreateEmployee(new TestEmployeeType(), name, payRate);
            decimal actualPayRate = Privateer.GetPayRateTestEmployee(employee);

            employee.Should().BeOfType<TestEmployee>();
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
        public void Test_NullEmployee_LogPayment_Does_Nothing()
        {
            string name = string.Empty;
            decimal payRate = 65.52M;
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new NullEmployeeType(), name, payRate);
            decimal pay = 0.00M;

            subject.LogPayment(pay);
            IList<int> actualHoursWorked = Privateer.GetHoursWorkedNullEmployee(subject);

            actualHoursWorked.Should().BeEmpty();
            
        }

        [TestMethod]
        public void Test_NullEmployee_Pay_Returns_0_And_Does_Not_Add_Log()
        {
            string name = string.Empty;
            decimal payRate = 65.52M;
            IEmployee subject = _talentAcquisitionCoordinator.CreateEmployee(new NullEmployeeType(), name, payRate);
            decimal expectedPay = 0.00M;

            decimal actualPay = subject.Pay();
            IList<int> actualHoursWorked = Privateer.GetHoursWorkedNullEmployee(subject);

            actualHoursWorked.Should().BeEmpty();
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

            public static List<int> GetHoursWorkedTestEmployee(IEmployee employee)
            {
                FieldInfo? fieldInfo = typeof(TestEmployee).GetField("_hoursWorked", BindingFlags.NonPublic | BindingFlags.Instance);
                return (List<int>)(fieldInfo?.GetValue(employee) ?? new List<int>());
            }

            public static decimal GetPayRateTestEmployee(IEmployee employee)
            {
                FieldInfo? fieldInfo = typeof(TestEmployee).GetField("_payRate", BindingFlags.NonPublic | BindingFlags.Instance);
                return (decimal)(fieldInfo?.GetValue(employee) ?? 0.00M);
            }

        }
    }
}
