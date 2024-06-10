using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServicesProvider.Domain.Models
{
    public class Contract
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int UserId { get; set; }

        public int StatusId { get; set; }

        public int ServiceId { get; set; }

        public User? User { get; set; }

        public ContractStatus? Status { get; set; }

        public Service? Service { get; set; }

    }
}
