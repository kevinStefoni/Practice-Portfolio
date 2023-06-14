using EmployeeLibrary.EmployeeTypes;

namespace EmployeeLibrary
{
    public abstract class TalentAcquisitionCoordinator
    {
        public abstract IEmployee CreateEmployee(IEmployeeType employeeType, string name, decimal payRate);

        public abstract IEmployee CreateEmployee(IEmployeeType employeeType, string name, decimal payRate, IScheduleSentry scheduleSentry);

        public static TalentAcquisitionCoordinator AssignTalentAcquisitionSpecialist()
        {
            return new TalentAcquisitionSpecialist();
        }

    }
}
