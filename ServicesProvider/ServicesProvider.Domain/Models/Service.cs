using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace ServicesProvider.Domain.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ServiceCategory? Category { get; set; }

    }
}
