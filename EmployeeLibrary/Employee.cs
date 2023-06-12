namespace PracticePortfolio.Models
{
    public class Employee : IEmployee
    {
        private readonly decimal _payRate;
        private IList<int> _hoursWorked = new List<int>();

        public string Name { get; set; } = string.Empty; 

        public IList<string> PaymentLogger { get; set; } = new List<string>();

        internal Employee(string name, decimal payRate)
        {
            Name = name;
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
            _hoursWorked.Clear();

            return pay;
        }

        /// <summary>
        /// This is the wrap method. 
        /// </summary>
        public decimal Pay()
        {
            decimal amountToPay = CalculatePay();
            LogPayment(amountToPay);
            return amountToPay;
        }

        public void LogPayment(decimal amountToPay)
        {
            PaymentLogger.Add($"Employee was paid ${amountToPay}");
        }

        public override string ToString()
        {
            return $"{Name} earns ${_payRate:0.00}/hr.";
        }
    }
}
