using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesProvider.Data.Entities
{
    [Table("request_status")]
    public class RequestStatusEntity
    {
        [Column("rs_id")]
        public int Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        public List<RequestEntity> Requests { get; set; }
    }
}
