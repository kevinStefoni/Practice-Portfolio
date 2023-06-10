namespace PracticePortfolio.Models
{
    public interface IEmployee
    {

        public string Name { get; set; }

        public void AddHours(IList<int> newHoursWorked);

        public decimal CalculatePay();

        public decimal Pay();

        public void LogPayment(decimal amountToPay);



    }
}