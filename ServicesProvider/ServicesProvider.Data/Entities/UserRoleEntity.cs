using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesProvider.Persistence.Entities
{
    [Table("user_role")]
    public class UserRoleEntity
    {
        [Column("ur_id")]
        public int Id { get; set; }

        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; } = string.Empty;

        public List<UserEntity> Users { get; set; }
    }
}
