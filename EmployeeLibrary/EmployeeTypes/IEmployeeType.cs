namespace EmployeeLibrary.EmployeeTypes
{
    public interface IEmployeeType
    {
        IEmployee CreateEmployee(string name, decimal payRate);

    }
}
