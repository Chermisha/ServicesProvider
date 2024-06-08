using System.ComponentModel.DataAnnotations;

namespace ServicesProvider.Models
{
    public class ServiceCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Service> Services { get; set; }
    }
}
