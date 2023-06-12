using EmployeeLibrary.EmployeeTypes;

namespace EmployeeLibrary
{
    public class TalentAcquisitionSpecialist : TalentAcquisitionCoordinator
    {
        private readonly IList<IEmployeeType> _employmentTypes = new List<IEmployeeType>();

        public override void RegisterEmploymentType(IEmployeeType employeeType)
        {
            _employmentTypes.Add(employeeType);
        }

        public override void UnregisterAllEmploymentTypes()
        {
            _employmentTypes.Clear();
        }

        public override IEmployee CreateEmployee(IEmployeeType employeeType, string name, decimal payRate)
        {
            var matchingType = _employmentTypes.FirstOrDefault(type => type.GetType() == employeeType.GetType());
            if (matchingType != null)
            {
                return matchingType.CreateEmployee(name, payRate);
            }

            throw new ArgumentException($"Unsupported employee type: {employeeType}");
        }
    }
}
