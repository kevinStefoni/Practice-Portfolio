using EmployeeLibrary.EmployeeTypes;

namespace EmployeeLibrary
{
    public class TalentAcquisitionSpecialist : TalentAcquisitionCoordinator
    {
        public override IEmployee CreateEmployee(IEmployeeType employeeType, string name, decimal payRate)
        {
            return employeeType.CreateEmployee(name, payRate);
        }
    }
}
