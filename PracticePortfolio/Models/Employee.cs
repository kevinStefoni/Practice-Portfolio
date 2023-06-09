namespace PracticePortfolio.Models
{
    public class Employee
    {
        private readonly decimal _payRate;
        private IList<int> _hoursWorked = new List<int>();
        public IList<string> PaymentLogger = new List<string>();

        public Employee(decimal payRate)
        {
            _payRate = payRate;
        }

        public void AddHours(IList<int> newHoursWorked)
        {
            _hoursWorked = _hoursWorked.Concat(newHoursWorked).ToList();
        }

        public decimal CalculatePay()
        {
            decimal pay = 0.00M;

            foreach(int hours in _hoursWorked)
            {
                pay += hours * _payRate;
            }


            return pay;
        }

        /// <summary>
        /// This is the wrap method. 
        /// </summary>
        public void Pay()
        {
            decimal amountToPay = CalculatePay();
            LogPayment(amountToPay);
        }

        public void LogPayment(decimal amountToPay)
        {
            PaymentLogger.Add($"Employee was paid ${amountToPay}");
        }
    }
}
