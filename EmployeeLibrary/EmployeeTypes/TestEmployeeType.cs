namespace EmployeeLibrary.EmployeeTypes
{
    public class TestEmployeeType : IEmployeeType
    {
        public IEmployee CreateEmployee(string name, decimal payRate)
        {
            return new TestEmployee(name, payRate);
        }
    }
}
