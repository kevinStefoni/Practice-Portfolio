namespace EmployeeLibrary.EmployeeTypes
{
    public class NullEmployeeType : IEmployeeType
    {
        public IEmployee CreateEmployee(string name, decimal payRate)
        {
            return new NullEmployee();
        }

        public IEmployee CreateEmployee(string name, decimal payRate, IScheduleSentry scheduleSentry)
        {
            return new NullEmployee(scheduleSentry);
        }
    }
}
