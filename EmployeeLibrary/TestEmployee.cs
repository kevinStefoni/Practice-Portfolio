namespace EmployeeLibrary
{
    public class TestEmployee : Employee
    {
        public TestEmployee(string name, decimal payRate) : base(name, payRate)
        {
        }

        public IList<int> HoursWorked { 
            get { return _hoursWorked; } 
            set { _hoursWorked = value; } 
        }



    }
}
