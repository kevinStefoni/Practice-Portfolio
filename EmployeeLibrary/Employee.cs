namespace EmployeeLibrary
{
    public class Employee : IEmployee
    {
        private readonly decimal _payRate;
        private readonly IScheduleSentry _scheduleSentry;

        public string Name { get; set; }
        public IList<string> PaymentLogger { get; set; } = new List<string>();

        internal Employee(string name, decimal payRate) : this(name, payRate, new ScheduleSentry())
        {
        }

        internal Employee(string name, decimal payRate, IScheduleSentry scheduleSentry)
        {
            Name = name;
            _payRate = payRate;
            _scheduleSentry = scheduleSentry;
        }

        public void AddHours(IList<int> newHoursWorked)
        {
            _scheduleSentry.AddHours(newHoursWorked);
        }

        public decimal CalculatePay()
        {
            decimal pay = 0.00M;

            foreach(int hours in _scheduleSentry.HoursWorked)
            {
                pay += hours * _payRate;
            }
            _scheduleSentry.ClearHours();

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
