using System.ComponentModel.DataAnnotations;

namespace ServicesProvider.Domain.Models
{
    public class ServiceCategory
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public List<Service> Services { get; set; }
    }
}
