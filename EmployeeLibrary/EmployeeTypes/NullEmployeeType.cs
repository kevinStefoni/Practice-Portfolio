namespace EmployeeLibrary.EmployeeTypes
{
    public class NullEmployeeType : IEmployeeType
    {
        public IEmployee CreateEmployee(string name, decimal payRate)
        {
            return new NullEmployee();
        }
    }
}
