namespace EmployeeLibrary.EmployeeTypes
{
    public class EmployeeType : IEmployeeType
    {
        public IEmployee CreateEmployee(string name, decimal payRate)
        {
            return new Employee(name, payRate);
        }

        public IEmployee CreateEmployee(string name, decimal payRate, IScheduleSentry scheduleSentry)
        {
            return new Employee(name, payRate, scheduleSentry);
        }
    }
}
