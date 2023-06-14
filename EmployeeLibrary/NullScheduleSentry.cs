namespace EmployeeLibrary
{
    public class NullScheduleSentry : IScheduleSentry
    {
        public void AddHours(IList<int> newHoursWorked)
        {
        }

        public void ClearHours()
        {
        }

        public IList<int> HoursWorked { get; } = new List<int>();
    }
}
