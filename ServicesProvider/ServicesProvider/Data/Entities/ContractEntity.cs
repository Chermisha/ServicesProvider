using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ServicesProvider.Data.Entities
{
    [Table("contract")]
    public class ContractEntity
    {
        [Column("c_id")]
        public int Id { get; set; }

        [Required]
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("status_id")]
        public int StatusId { get; set; }

        [Column("service_id")]
        public int ServiceId { get; set; }

        public UserEntity? User { get; set; }

        public ContractStatusEntity? Status { get; set; }

        public ServiceEntity? Service { get; set; }

    }
}
