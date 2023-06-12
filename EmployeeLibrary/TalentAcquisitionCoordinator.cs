using EmployeeLibrary.EmployeeTypes;

namespace EmployeeLibrary
{
    public abstract class TalentAcquisitionCoordinator
    {
        public abstract IEmployee CreateEmployee(IEmployeeType employeeType,string name, decimal payRate);

        public abstract void RegisterEmploymentType(IEmployeeType employeeType);

        public abstract void UnregisterAllEmploymentTypes();

        public static TalentAcquisitionCoordinator AssignTalentAcquisitionSpecialist()
        {
            return new TalentAcquisitionSpecialist();
        }

    }
}
