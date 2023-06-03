namespace PracticePortfolio.Models.Entities
{
    public class User : IUser
    {
        public int Id { get; set; } = -1;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;


    }
}
