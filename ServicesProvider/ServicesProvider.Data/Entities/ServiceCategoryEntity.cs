using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesProvider.Persistence.Entities
{
    [Table("service_category")]
    public class ServiceCategoryEntity
    {
        [Column("sc_id")]
        public int Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; } = string.Empty;

        public List<ServiceEntity> Services { get; set; }
    }
}
