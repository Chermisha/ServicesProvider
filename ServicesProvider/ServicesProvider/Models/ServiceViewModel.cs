using ServicesProvider.Domain.Models;

namespace ServicesProvider.Models
{
    public class ServiceViewModel
    {
        public required int Id { get; set; }

        public required string Name { get; set; } 

        public string Description { get; set; } = string.Empty;

        public required decimal Price { get; set; }

        public required string Category { get; set; } 
    }
}
