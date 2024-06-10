using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace ServicesProvider.Domain.Models
{
    public class Request
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string Comment { get; set; } = string.Empty;

        public int StatusId { get; set; }

        public int UserId { get; set; }

        public int ServiceId { get; set; }

        public RequestStatus? Status { get; set; }

        public User? User { get; set; }

        public Service? Service { get; set; }

    }
}
