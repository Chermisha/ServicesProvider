using System.ComponentModel.DataAnnotations;

namespace ServicesProvider.Domain.Models
{
    public class RequestStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Request> Requests { get; set; }
    }
}
