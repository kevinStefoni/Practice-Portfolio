namespace PracticePortfolio.Models.Employee_Models
{
    public abstract class EmployeeFactory
    {
        public abstract IEmployee CreateEmployee(string name, decimal payRate);

        public static EmployeeFactory GetFactory()
        {
            return new ConcreteEmployeeFactory();
        }

    }
}
