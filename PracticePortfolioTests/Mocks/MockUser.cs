using PracticePortfolio.Models.Entities;

namespace PracticePortfolioTests.Mocks
{
    public class MockUser : IUser
    {
        public int Id { get; set; } = 123;

        public string Name { get; set; } = "John Doe";

        public string Email { get; set; } = "test@gmail.com";

        public string Password { get; set; } = "tropicalautomobilegreentree";

    }
}
