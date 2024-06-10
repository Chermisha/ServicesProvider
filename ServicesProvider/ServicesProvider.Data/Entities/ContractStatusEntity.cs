using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesProvider.Persistence.Entities
{
    [Table("contract_status")]
    public class ContractStatusEntity
    {
        [Column("cs_id")]
        public int Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; } = string.Empty;

        public List<ContractEntity> Contracts { get; set; }
    }
}
