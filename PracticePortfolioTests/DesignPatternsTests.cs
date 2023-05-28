using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Controllers;
using PracticePortfolio.Models;

namespace PracticePortfolioTests
{
    public class DesignPatterns
    {
        [Fact]
        public void Assert_Singleton_Return_Singleton_Data_Type()
        { 
            Assert.IsType<Singleton_>(Singleton_.GetInstance());
        }

        [Fact]
        public void Assert_Singleton_Return_First_Time_Not_Null()
        {
            Assert.NotNull(Singleton_.GetInstance());
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
        public void Assert_Singleton_One_Instance_Change_Value(int value)
        {
            var firstSingleton = Singleton_.GetInstance();
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
        public void Assert_Singleton_Two_Instances_Change_Value(int value, int newValue)
        {
            var firstSingleton = Singleton_.GetInstance();
            firstSingleton.Value = value;

            var secondSingleton = Singleton_.GetInstance();
            secondSingleton.Value = newValue;

            Assert.Equal(newValue, firstSingleton.Value);
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
            IActionResult result = new DesignPatternsController().SingletonDemo(value, newValue);
            SingletonPair? sp = ((OkObjectResult) result).Value as SingletonPair;

            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(sp);
            Assert.Equal(newValue, sp.FirstInstance.Value);
            Assert.Equal(sp.SecondInstance.Value, sp.FirstInstance.Value);
        }


    }
}