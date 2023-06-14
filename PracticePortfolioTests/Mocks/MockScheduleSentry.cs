using EmployeeLibrary;

namespace PracticePortfolioTests.Mocks
{
    internal class MockScheduleSentry : IScheduleSentry
    {
        public IList<int> HoursWorked { get; set; }

        public MockScheduleSentry()
        {
            HoursWorked = new List<int>();
        }

        public MockScheduleSentry(IList<int> hoursWorked)
        {
            HoursWorked = hoursWorked;
        }

        public void AddHours(IList<int> newHoursWorked)
        {
            throw new NotImplementedException();
        }

        public void ClearHours()
        {
            HoursWorked.Clear();
        }
    }
}
