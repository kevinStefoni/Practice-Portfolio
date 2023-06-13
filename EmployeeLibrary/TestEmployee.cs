namespace EmployeeLibrary
{
    public class TestEmployee : Employee
    {
        public TestEmployee(string name, decimal payRate) : base(name, payRate)
        {
        }

        public IList<int> HoursWorked { 
            set { _hoursWorked = value; } 
        }



    }
}
