using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace ServicesProvider.Persistence.Entities
{
    [Table("request")]
    public class RequestEntity
    {
        [Column("r_id")]
        public int Id { get; set; }

        [Required]
        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Column("comment", TypeName = "varchar(1000)")]
        public string Comment { get; set; } = string.Empty;

        [Column("status_id")]
        public int StatusId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("service_id")]
        public int ServiceId { get; set; }

        public RequestStatusEntity? Status { get; set; }

        public UserEntity? User { get; set; }

        public ServiceEntity? Service { get; set; }

    }
}
