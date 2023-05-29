using Moq;

using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Controllers;
using PracticePortfolio.Models;

namespace PracticePortfolioTests
{
    public class DesignPatterns
    {
        [Fact]
        public void Singleton_Return_Singleton_Data_Type()
        {
            Singleton singleton = Singleton.GetInstance();

            Assert.IsType<Singleton>(singleton);
        }

        [Fact]
        public void Singleton_Return_First_Time_Not_Null()
        {
            Singleton singleton = Singleton.GetInstance();

            Assert.NotNull(singleton);
        }

        [Theory]
        [InlineData (0)]
        [InlineData (1)]
        [InlineData (-1)]        
        [InlineData (5000)]
        [InlineData (-5000)]
        [InlineData (-2147483648)]
        [InlineData (2147483647)]        
        [InlineData (-2147483647)]
        [InlineData (2147483646)]
        public void Singleton_One_Instance_Change_Value(int value)
        {
            Singleton firstSingleton = Singleton.GetInstance();

            firstSingleton.Value = value;

            Assert.Equal(value, firstSingleton.Value);
        }

        [Theory]
        [InlineData(0, 50)]
        [InlineData(1, 50)]
        [InlineData(-1, 50)]
        [InlineData(5000, 50)]
        [InlineData(-5000, 50)]
        [InlineData(-2147483648, 50)]
        [InlineData(2147483647, 50)]
        [InlineData(-2147483647, 50)]
        [InlineData(2147483646, 50)]
        [InlineData(50, 0)]
        [InlineData(50, 1)]
        [InlineData(50, -1)]
        [InlineData(50, 5000)]
        [InlineData(50, -5000)]
        [InlineData(50, -2147483648)]
        [InlineData(50, 2147483647)]
        [InlineData(50, -2147483647)]
        [InlineData(50, 2147483646)]
        public void Singleton_Two_Instances_Change_Value(int value, int newValue)
        {
            Singleton firstSingleton = Singleton.GetInstance();
            Singleton secondSingleton = Singleton.GetInstance();

            firstSingleton.Value = value;
            secondSingleton.Value = newValue;

            Assert.Equal(newValue, firstSingleton.Value);
            Assert.Equal(secondSingleton.Value, firstSingleton.Value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(5000)]
        [InlineData(-5000)]
        [InlineData(-2147483648)]
        [InlineData(2147483647)]
        [InlineData(-2147483647)]
        [InlineData(2147483646)]
        [InlineData(50)]
        public void Singleton_Second_Instance_Immediately_Has_First_Instance_Value(int value)
        {
            Singleton firstSingleton = Singleton.GetInstance();
            Singleton secondSingleton = Singleton.GetInstance();

            firstSingleton.Value = value;

            Assert.Equal(secondSingleton.Value, firstSingleton.Value);
        }

        [Theory]
        [InlineData(0, 50)]
        [InlineData(1, 50)]
        [InlineData(-1, 50)]
        [InlineData(5000, 50)]
        [InlineData(-5000, 50)]
        [InlineData(-2147483648, 50)]
        [InlineData(2147483647, 50)]
        [InlineData(-2147483647, 50)]
        [InlineData(2147483646, 50)]
        [InlineData(50, 0)]
        [InlineData(50, 1)]
        [InlineData(50, -1)]
        [InlineData(50, 5000)]
        [InlineData(50, -5000)]
        [InlineData(50, -2147483648)]
        [InlineData(50, 2147483647)]
        [InlineData(50, -2147483647)]
        [InlineData(50, 2147483646)]
        public void Singleton_Demo_Return_Same_Value(int value, int newValue)
        {
            DesignPatternsController designPatternsController = new();

            IActionResult result = designPatternsController.SingletonDemo(value, newValue);
            SingletonPair? singletonPair = ((OkObjectResult) result).Value as SingletonPair;

            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(singletonPair);
            Assert.Equal(newValue, singletonPair.FirstInstance.Value);
            Assert.Equal(singletonPair.SecondInstance.Value, singletonPair.FirstInstance.Value);
        }

        [Theory]
        [InlineData("54.31,1234567812345678,123")]
        [InlineData("5999,1234567812345678,123")]
        [InlineData("0,1234567812345678,123")]
        [InlineData("0.00,1234567812345678,123")]
        [InlineData("0.01,1234567812345678,123")]
        [InlineData("0.2,1234567812345678,123")]
        [InlineData("5.,1234567812345678,123")]
        [InlineData("79228162514264337593543950335,1234567812345678,123")]
        [InlineData("79228162514264337593543950334.99,1234567812345678,123")]
        public void LegacyPaymentGateway_Returns_Payment_Statement(string paymentData)
        {
            Mock<ILegacyPaymentDataValidator> mockValidator = new();
            mockValidator.Setup(validator => validator.IsPaymentDataValid(It.IsAny<string>()))
                         .Returns(true);
            IPaymentGateway legacyPaymentGateway = new LegacyPaymentGateway(mockValidator.Object);


            string paymentStatement = legacyPaymentGateway.ProcessPayment(paymentData);

            Assert.Equal($"A payment was made using the legacy system with the following data: {paymentData}.", paymentStatement);
        }

        [Theory]
        [InlineData("-10.25,1234567812345678,123")]
        [InlineData("0.001,1234567812345678,123")]
        [InlineData("79228162514264337593543950334.999,1234567812345678,123")]
        [InlineData("0.0000000000001,1234567812345678,123")]
        public void LegacyPaymentValidator_Invalid_Amount_Returns_False(string paymentData)
        {
            ILegacyPaymentDataValidator legacyPaymentDataValidator = new LegacyPaymentDataValidator();

            bool validationResult = legacyPaymentDataValidator.IsPaymentDataValid(paymentData);

            Assert.False(validationResult);
        }

        [Theory]
        [InlineData("54.31,123456781234567,123")]
        [InlineData("54.31,12345678123456777,123")]
        [InlineData("54.31,1234567A123456777,123")]
        [InlineData("54.31,1234567_123456777,123")]
        [InlineData("54.31,12 34567123456777,123")]
        [InlineData("54.31,1234-6789-1485-1841,123")]
        [InlineData("54.31,,123")]
        public void LegacyPaymentValidator_Invalid_Card_Number_Returns_False(string paymentData)
        {
            ILegacyPaymentDataValidator legacyPaymentDataValidator = new LegacyPaymentDataValidator();

            bool validationResult = legacyPaymentDataValidator.IsPaymentDataValid(paymentData);

            Assert.False(validationResult);
        }

        [Theory]
        [InlineData("54.31,123456781234567,1237")]
        [InlineData("54.31,12345678123456777,23")]
        [InlineData("54.31,1234567A123456777,")]
        [InlineData("54.31,1234567_123456777,12A3")]
        [InlineData("54.31,12 34567123456777,ABC")]
        [InlineData("54.31,1234-6789-1485-1841,#81")]
        public void LegacyPaymentValidator_Invalid_Cvv_Returns_False(string paymentData)
        {
            ILegacyPaymentDataValidator legacyPaymentDataValidator = new LegacyPaymentDataValidator();

            bool validationResult = legacyPaymentDataValidator.IsPaymentDataValid(paymentData);

            Assert.False(validationResult);
        }

        [Theory]
        [InlineData("54.31,1234567812345678,123")]
        [InlineData("5999,1234567812345678,123")]
        [InlineData("0,1234567812345678,123")]
        [InlineData("0.00,1234567812345678,123")]
        [InlineData("0.01,1234567812345678,123")]
        [InlineData("0.2,1234567812345678,123")]
        [InlineData("5.,1234567812345678,123")]
        [InlineData("79228162514264337593543950335,1234567812345678,123")]
        [InlineData("79228162514264337593543950334.99,1234567812345678,123")]
        public void LegacyPaymentValidator_Valid_Data_Returns_true(string paymentData)
        {
            ILegacyPaymentDataValidator legacyPaymentDataValidator = new LegacyPaymentDataValidator();

            bool validationResult = legacyPaymentDataValidator.IsPaymentDataValid(paymentData);

            Assert.True(validationResult);
        }

        [Theory]
        [InlineData ("54.31,1234567812345678,123")]
        [InlineData ("5999,1234567812345678,123")]
        [InlineData ("0,1234567812345678,123")]
        [InlineData ("0.00,1234567812345678,123")]
        [InlineData ("0.01,1234567812345678,123")]
        [InlineData ("0.2,1234567812345678,123")]
        [InlineData ("5.,1234567812345678,123")]
        [InlineData ("79228162514264337593543950335,1234567812345678,123")]
        [InlineData ("79228162514264337593543950334.99,1234567812345678,123")]
        public void Run_LegacyPaymentGateway_Returns_Payment_Statement(string paymentData)
        {
            Adapter adapter = new();

            string paymentStatement = adapter.Run(paymentData);

            Assert.Equal($"A payment was made using the legacy system with the following data: {paymentData}.", paymentStatement);
        } 

        [Theory]
        [InlineData("54.31","1234567812345678","123")]
        [InlineData("5999","1234567812345678","123")]
        [InlineData("0","1234567812345678","123")]
        [InlineData("0.00","1234567812345678","123")]
        [InlineData("0.01","1234567812345678","123")]
        [InlineData("0.2","1234567812345678","123")]
        [InlineData("5.0","1234567812345678","123")]
        [InlineData("79228162514264337593543950335","1234567812345678","123")]
        [InlineData("79228162514264337593543950334.99","1234567812345678","123")]
        public void NewPaymentGateway_Returns_Payment_Statement(string amount, string cardNumber, string cvv)
        {
            decimal amt = 0M;

            try
            {
                amt = Convert.ToDecimal(amount);
            }
            catch
            {
                Assert.Fail("Invalid amount provided to unit test.");
            }
            Mock<INewPaymentDataValidator> mockValidator = new();
            mockValidator.Setup(validator => validator.IsPaymentDataValid(It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<string>()))
                         .Returns(true);
            INewPaymentGateway newPaymentGateway = new NewPaymentGateway(mockValidator.Object);
            
            string paymentStatement = newPaymentGateway.MakePayment(amt, cardNumber, cvv);
            Assert.Equal($"A ${amt:F2} payment was made using a credit card with the credit card number {cardNumber} and a CVV of {cvv} using the new payment gateway.", paymentStatement);

        }

        [Theory]
        [InlineData("-10.25","1234567812345678","123")]
        [InlineData("0.001","1234567812345678","123")]
        [InlineData("0.0000000000054278","1234567812345678","123")]
        public void NewPaymentValidator_Invalid_Amount_Returns_False(string amount, string cardNumber, string cvv)
        {
            decimal amt = 0M;

            try
            {
                amt = Convert.ToDecimal(amount);
            }
            catch
            {
                Assert.Fail("Invalid amount provided to unit test.");
            }
            INewPaymentDataValidator newPaymentDataValidator = new NewPaymentDataValidator();

            bool validationResult = newPaymentDataValidator.IsPaymentDataValid(amt, cardNumber, cvv);
            Assert.False(validationResult);

        }

        [Theory]
        [InlineData("54.31","123456781234567","123")]
        [InlineData("54.31","12345678123456777","123")]
        [InlineData("54.31","1234567A123456777","123")]
        [InlineData("54.31","1234567_123456777","123")]
        [InlineData("54.31","12 34567123456777","123")]
        [InlineData("54.31","1234-6789-1485-1841","123")]
        [InlineData("54.31","","123")]
        public void NewPaymentValidator_Invalid_Card_Number_Returns_False(string amount, string cardNumber, string cvv)
        {
            decimal amt = 0M;

            try
            {
                amt = Convert.ToDecimal(amount);
            }
            catch
            {
                Assert.Fail("Invalid amount provided to unit test.");
            }
            INewPaymentDataValidator newPaymentDataValidator = new NewPaymentDataValidator();

            bool validationResult = newPaymentDataValidator.IsPaymentDataValid(amt, cardNumber, cvv);
            Assert.False(validationResult);

        }

        [Theory]
        [InlineData("54.31", "1234567812345678", "1237")]
        [InlineData("5999", "1234567812345678", "23")]
        [InlineData("0", "1234567812345678", "")]
        [InlineData("0.00", "1234567812345678", "123A3")]
        [InlineData("0.01", "1234567812345678", "ABC")]
        [InlineData("0.2", "1234567812345678", "#13")]
        public void NewPaymentValidator_Invalid_Cvv_Returns_False(string amount, string cardNumber, string cvv)
        {
            decimal amt = 0M;

            try
            {
                amt = Convert.ToDecimal(amount);
            }
            catch
            {
                Assert.Fail("Invalid amount provided to unit test.");
            }
            INewPaymentDataValidator newPaymentDataValidator = new NewPaymentDataValidator();

            bool validationResult = newPaymentDataValidator.IsPaymentDataValid(amt, cardNumber, cvv);
            Assert.False(validationResult);

        }

        [Theory]
        [InlineData("54.31", "1234567812345678", "123")]
        [InlineData("5999", "1234567812345678", "123")]
        [InlineData("0", "1234567812345678", "123")]
        [InlineData("0.00", "1234567812345678", "123")]
        [InlineData("0.01", "1234567812345678", "123")]
        [InlineData("0.2", "1234567812345678", "123")]
        [InlineData("5.0", "1234567812345678", "123")]
        [InlineData("79228162514264337593543950335", "1234567812345678", "123")]
        [InlineData("79228162514264337593543950334.99", "1234567812345678", "123")]
        public void NewPaymentValidator_Valid_Data_Returns_True(string amount, string cardNumber, string cvv)
        {
            decimal amt = 0M;

            try
            {
                amt = Convert.ToDecimal(amount);
            }
            catch
            {
                Assert.Fail("Invalid amount provided to unit test.");
            }
            INewPaymentDataValidator newPaymentDataValidator = new NewPaymentDataValidator();

            bool validationResult = newPaymentDataValidator.IsPaymentDataValid(amt, cardNumber, cvv);
            Assert.True(validationResult);

        }

    }
}