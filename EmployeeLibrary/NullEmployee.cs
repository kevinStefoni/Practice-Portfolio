namespace PracticePortfolio.Models.Employee_Models
{
    public class NullEmployee : IEmployee
    {
        private readonly decimal _payRate = 0.00M;

        public string Name { get; set; } = string.Empty;
        public IList<string> PaymentLogger { get; set; } = new List<string>();

        internal NullEmployee() { }

        public void AddHours(IList<int> newHoursWorked)
        {
            
        }

        public decimal CalculatePay()
        {
            return 0.00M;
        }

        public void LogPayment(decimal amountToPay)
        {
            
        }

        public decimal Pay()
        {
            decimal amountToPay = CalculatePay();
            LogPayment(amountToPay);
            return amountToPay;
        }
    }
}
