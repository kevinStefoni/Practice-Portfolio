using PracticePortfolio.Models.DTOs;

namespace PracticePortfolioTests.Mocks
{
    public class MockUserDTO : IUserDTO
    {
        public string Name { get; set; } = "John Doe";

        public string Email { get; set; } = "test@gmail.com";
    }
}
