namespace EmployeeLibrary.EmployeeTypes
{
    public class EmployeeType : IEmployeeType
    {
        public IEmployee CreateEmployee(string name, decimal payRate)
        {
            return new Employee(name, payRate);
        }
    }
}
