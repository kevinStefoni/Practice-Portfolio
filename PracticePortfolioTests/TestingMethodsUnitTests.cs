﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticePortfolio.Models;
using System.Reflection;

namespace PracticePortfolioTests
{
    [TestClass]
    public class TestingMethodsUnitTests
    {

        [TestMethod]
        public void Test_Create_Employee_Object()
        {
            decimal payRate = 15.25M;

            Employee subject = new(payRate);

            subject.Should().NotBeNull();
            subject.Should().BeOfType<Employee>();
        }

        [TestMethod]
        public void Test_Pay_No_Hours_Worked_Returns_0()
        {
            decimal payRate = 15.25M;
            Employee subject = new(payRate);

            decimal pay = subject.CalculatePay();

            pay.Should().Be(0);
        }

        [TestMethod]
        public void Test_Pay_1_Hour_Worked_In_One_Day_Returns_Pay_Rate()
        {
            IList<int> dailyHoursWorked = new List<int>() { 1 };
            decimal payRate = 15.25M;
            Employee subject = new(payRate);
            subject.AddHours(dailyHoursWorked);
            decimal expectedPay = payRate;

            decimal pay = subject.CalculatePay();

            pay.Should().Be(expectedPay);
        }

        [TestMethod]
        public void Test_Pay_Hours_Worked_In_One_Day_Returns_Correct_Pay()
        {
            int hoursInOneDay = 5;
            IList<int> dailyHoursWorked = new List<int>() { hoursInOneDay };
            decimal payRate = 15.25M;
            Employee subject = new(payRate);
            subject.AddHours(dailyHoursWorked);
            decimal expectedPay = hoursInOneDay * payRate;


            decimal pay = subject.CalculatePay();

            pay.Should().Be(expectedPay);
        }

        [TestMethod]
        public void Test_Pay_Hours_Worked_In_Multiple_Days_Returns_Correct_Pay()
        {
            IList<int> dailyHoursWorked = new List<int>() { 8, 8, 7, 8, 0 };
            decimal payRate = 15.25M;
            Employee subject = new(payRate);
            subject.AddHours(dailyHoursWorked);
            int totalHours = dailyHoursWorked.Sum(t => t);
            decimal expectedPay = totalHours * payRate;

            decimal pay = subject.CalculatePay();

            pay.Should().Be(expectedPay);
        }

        [TestMethod]
        public void Test_Log_Payment_Returns_Payment_Confirmation_Statement_With_Correct_Amount()
        {
            IList<int> dailyHoursWorked = new List<int>() { 8, 8, 7, 8, 0 };
            decimal payRate = 15.25M;
            Employee employee = new(payRate);
            employee.AddHours(dailyHoursWorked);
            int totalHours = dailyHoursWorked.Sum(t => t);
            decimal pay = totalHours * payRate;
            string expectedPaymentStatement = $"Employee was paid ${pay}";

            employee.LogPayment(pay);
            IList<string> subject = employee.PaymentLogger;

            subject.Should().OnlyContain(actualStatement => actualStatement == expectedPaymentStatement);
           
        }

        [TestMethod]
        public void Test_Add_Week_Of_Hours()
        {
            IList<int> hoursWorkedLastWeek = new List<int>() { 0, 8, 8, 7, 4, 8, 0 };
            IList<int> expectedHoursWorked = hoursWorkedLastWeek;
            decimal payRate = 15.25M;
            Employee employee = new(payRate);

            employee.AddHours(hoursWorkedLastWeek);
            Privateer privateer = new();
            IList<int> subject = privateer.GetHoursWorked(employee);

            subject.Should().BeEquivalentTo(expectedHoursWorked);
            

        }

        public class Privateer
        {
            public List<int> GetHoursWorked(Employee employee)
            {
                var fieldInfo = typeof(Employee).GetField("_hoursWorked", BindingFlags.NonPublic | BindingFlags.Instance);
                return (List<int>) (fieldInfo?.GetValue(employee) ?? new List<int>());
            }
        }


    }
}