namespace EmployeeLibrary
{
    public interface IScheduleSentry
    {
        IList<int> HoursWorked { get; }
        void AddHours(IList<int> newHoursWorked);
        void ClearHours();
        
    }
}
