namespace PracticePortfolio.Models.Employee_Models
{
    public class ConcreteEmployeeFactory : EmployeeFactory
    {
        public override IEmployee CreateEmployee(string name, decimal payRate)
        {
            if(string.IsNullOrEmpty(name))
            {
                return new NullEmployee();
            }
            else
            {
                return new Employee(name, payRate);
            }
        }
    }
}
