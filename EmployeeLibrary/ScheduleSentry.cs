namespace EmployeeLibrary
{
    public class ScheduleSentry : IScheduleSentry
    {
        private IList<int> _hoursWorked = new List<int>();

        public void AddHours(IList<int> newHoursWorked)
        {
            _hoursWorked = _hoursWorked.Concat(newHoursWorked).ToList();
        }

        public void ClearHours()
        {
            _hoursWorked.Clear();
        }

        public IList<int> HoursWorked { 
            get { return _hoursWorked; }
        }
    }
}
