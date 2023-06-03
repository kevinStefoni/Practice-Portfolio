using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PracticePortfolio.Controllers;
using PracticePortfolio.Models;
using PracticePortfolio.Models.DTOs;
using PracticePortfolio.Models.Entities;
using PracticePortfolioTests.Mocks;

namespace PracticePortfolioTests
{
    public class OperationsUnitTests
    {
        [Fact]
        public void Create_And_Set_Value_Of_ImperialPounds_Object()
        {
            ImperialPound subject = new(5.5259);
            subject.Should().BeOfType<ImperialPound>();
            subject.Pounds.Should().Be(5.5259);
        }

        [Fact]
        public void Convert_Kilogram_To_ImperialPound()
        {
            double kilograms = 3.245;
            double expectedImperialWeight = kilograms * 2.20462;

            ImperialPound subject = (ImperialPound) kilograms;

            subject.Pounds.Should().Be(expectedImperialWeight);

        }

        [Fact]
        public void ExplicitOperatorDemo_Returns_OkObjectResult()
        {
            CsharpController csharpController = new();

            IActionResult subject = csharpController.ExplicitOperatorDemo(0);

            subject.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public void Create_User_And_Set_And_Get_Values()
        {
            IUser mockUser = new MockUser();
            User subject = new()
            {
                Id = mockUser.Id,
                Name = mockUser.Name,
                Email = mockUser.Email,
                Password = mockUser.Password
            };

            subject.Should().NotBeNull();
            subject.Should().BeOfType<User>();
            subject.Id.Should().Be(mockUser.Id);
            subject.Name.Should().Be(mockUser.Name);
            subject.Email.Should().Be(mockUser.Email);
            subject.Password.Should().Be(mockUser.Password);

        }

        [Fact]
        public void Create_Uninitialized_User()
        {
            User subject = new();

            subject.Id.Should().Be(-1);
            subject.Name.Should().Be(string.Empty);
            subject.Email.Should().Be(string.Empty);
            subject.Password.Should().Be(string.Empty);

        }

        [Fact]
        public void Create_UserDTO_And_Get_And_Set_Values()
        {
            IUserDTO userDTO = new MockUserDTO();
            UserDTO subject = new()
            {
                Name = userDTO.Name,
                Email = userDTO.Email
            };

            subject.Should().NotBeNull();
            subject.Should().BeOfType<UserDTO>();
            subject.Name.Should().Be(userDTO.Name);
            subject.Email.Should().Be(userDTO.Email);
            subject.Should().NotBeAssignableTo<User>();

        }

        [Fact]
        public void Convert_User_To_UserDTO_Implicit_Conversion()
        {
            IUser mockUser = new MockUser();
            User user = new()
            {
                Id = mockUser.Id,
                Name = mockUser.Name,
                Email = mockUser.Email,
                Password = mockUser.Password
            };

            UserDTO subject = user;

            subject.Should().NotBeEquivalentTo(user);
            subject.Name.Should().Be(user.Name);
            subject.Email.Should().Be(user.Email);
        }

        [Fact]
        public void ImplicitOperatorDemo_Returns_OkObjectResult()
        {
            CsharpController csharpController = new();

            IActionResult subject = csharpController.ImplicitOperatorDemo();

            subject.Should().BeOfType<OkObjectResult>();

        }


    }
}
