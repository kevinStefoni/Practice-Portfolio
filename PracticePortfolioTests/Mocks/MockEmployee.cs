using PracticePortfolio.Models;

namespace PracticePortfolioTests.Mocks
{
    public class MockEmployee : IEmployee
    {
        public string Name { get; set; } = "John Doe";

        public IList<string> PaymentLogger { get; set; } = new List<string>();

        public void AddHours(IList<int> newHoursWorked)
        {
            throw new NotImplementedException();
        }

        public decimal CalculatePay()
        {
            throw new NotImplementedException();
        }

        public void LogPayment(decimal amountToPay)
        {
            throw new NotImplementedException();
        }

        public decimal Pay()
        {
            throw new NotImplementedException();
        }
    }
}
