using EmployeeLibrary.EmployeeTypes;

namespace EmployeeLibrary
{
    public abstract class TalentAcquisitionCoordinator
    {
        public abstract IEmployee CreateEmployee(IEmployeeType employeeType,string name, decimal payRate);

        public static TalentAcquisitionCoordinator AssignTalentAcquisitionSpecialist()
        {
            return new TalentAcquisitionSpecialist();
        }

    }
}
