using Microsoft.AspNetCore.Identity;
using ServicesProvider.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace ServicesProvider.Data.Entities
{
    [Table("user")]
    public class UserEntity
    {
        [Column("u_id")]
        public int Id { get; set; }

        [Column("last_name", TypeName = "varchar(100)")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column("first_name", TypeName = "varchar(100)")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Column("middle_name", TypeName = "varchar(100)")]
        public string MiddleName { get; set; } = string.Empty;

        [Column("date_of_birth")]
        public DateOnly DateOfBirth { get; set; }

        [Column("adress", TypeName = "varchar(100)")]
        public string Address { get; set; } = string.Empty; 

        [Column("phone", TypeName = "varchar(50)")]
        public string Phone { get; set; } = string.Empty; 

        [Required]
        [Column("email", TypeName = "varchar(150)")]
        public string Email { get; set; }

        [Required]
        [Column("password", TypeName = "varchar(150)")]
        public string Password { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        public UserRoleEntity? Role { get; set; }

        public List<RequestEntity> Requests { get; set; }

        public List<ContractEntity> Contracts { get; set; }
    }
}
