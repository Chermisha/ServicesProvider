using ServicesProvider.Domain.Enums;
using ServicesProvider.Domain.Models;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace ServicesProvider.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string LastName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } 

        public string Password { get; set; }

        public UserRole Role { get; set; }//УБРАТЬ ПОЛЯ НИЖЕ?

        public List<Request> Requests { get; set; }

        public List<Contract> Contracts { get; set; }
    }
}
