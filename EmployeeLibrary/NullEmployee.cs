namespace EmployeeLibrary
{
    public class NullEmployee : IEmployee
    {
        private readonly decimal _payRate = 0.00M;
        private readonly IScheduleSentry _scheduleSentry;

        public string Name { get; set; } = string.Empty;
        public IList<string> PaymentLogger { get; set; } = new List<string>();

        internal NullEmployee(IScheduleSentry scheduleSentry)
        {
            _scheduleSentry = scheduleSentry;
        }

        internal NullEmployee() : this(new NullScheduleSentry())
        {
        }

        public void AddHours(IList<int> newHoursWorked)
        {
            _scheduleSentry.AddHours(newHoursWorked);
        }

        public decimal CalculatePay() => 0.00M;

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
