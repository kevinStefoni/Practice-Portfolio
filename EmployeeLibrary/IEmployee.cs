namespace EmployeeLibrary
{
    public interface IEmployee
    {
        string Name { get; set; }

        IList<string> PaymentLogger { get; set; }

        void AddHours(IList<int> newHoursWorked);

        decimal CalculatePay();

        decimal Pay();

        void LogPayment(decimal amountToPay);

    }
}
