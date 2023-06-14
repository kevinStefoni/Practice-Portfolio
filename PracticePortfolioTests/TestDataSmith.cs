using EmployeeLibrary;
using EmployeeLibrary.EmployeeTypes;

namespace PracticePortfolioTests
{
    public static class TestDataSmith
    {
        private static readonly TalentAcquisitionCoordinator _talentAcquisitionCoordinator = TalentAcquisitionCoordinator.AssignTalentAcquisitionSpecialist();

        public static IEnumerable<object[]> EmployeeData()
        {
            yield return new object[] { "John Doe", 15.25M };
        }

        public static IEnumerable<object[]> MultipleEmployeesTestData()
        {
            yield return new object[]
            {
                new IEmployee[]
                {
                    _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(),"John Doe", 15.25M),
                    _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), "Jane Smith", 20.50M),
                    _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), "Nicolas Garcia", 19.25M),
                    _talentAcquisitionCoordinator.CreateEmployee(new EmployeeType(), "Gracie Smith", 17.25M)
                }
            };
        }

        public static IEnumerable<object[]> MultipleSetsOfHours()
        {
            yield return new object[]
            {
                new IList<int>[]
                {
                    new List<int>() { 0, 8, 6, 10, 0, 0 },
                    new List<int>() { 0, 8, 8, 8, 8, 8, 0 },
                    new List<int>() { 0, 8, 12, 8, 10, 8, 0 },
                }
            };
        }

        public static IEnumerable<object[]> EmployeeWithMultipleSetsOfHours()
        {

            yield return new object[]
            {
                "John Doe",
                13.00M,
                new IList<int>[]
                {
                    new List<int>() { 0, 8, 6, 10, 0, 0 },
                    new List<int>() { 0, 8, 8, 8, 8, 8, 0 },
                    new List<int>() { 0, 8, 12, 8, 10, 8, 0 },
                }
            };
        }

        public static IEnumerable<object[]> EmployeeWithOneSetOfHours()
        {

            yield return new object[]
            {
                "John Doe",
                13.00M,
                new List<int>() { 0, 8, 6, 10, 0, 0 }
            };
        }

    }
}
