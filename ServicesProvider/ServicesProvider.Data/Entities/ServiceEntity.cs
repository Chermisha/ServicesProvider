using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace ServicesProvider.Persistence.Entities
{
    [Table("service")]
    public class ServiceEntity
    {
        [Column("s_id")]
        public int Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column("description", TypeName = "varchar(1000)")]
        public string Description { get; set; }

        [Required]
        [Column("price", TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        public ServiceCategoryEntity? Category { get; set; }

        public List<ContractEntity> Contracts { get; set; }

    }
}
