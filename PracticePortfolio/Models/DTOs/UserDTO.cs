using PracticePortfolio.Models.Entities;

namespace PracticePortfolio.Models.DTOs
{
    public class UserDTO : IUserDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public static implicit operator UserDTO(User user)
        {
            return new UserDTO() { 
                Name = user.Name, 
                Email = user.Email 
            };

        }


    }
}
