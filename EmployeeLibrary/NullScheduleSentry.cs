namespace EmployeeLibrary
{
    public class NullScheduleSentry : IScheduleSentry
    {
        private readonly IList<int> _hoursWorked = new List<int>();

        public void AddHours(IList<int> newHoursWorked)
        {
        }

        public void ClearHours()
        {
        }

        public IList<int> HoursWorked
        {
            get { return _hoursWorked; }
        }
    }
}
