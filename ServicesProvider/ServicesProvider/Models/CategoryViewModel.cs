using ServicesProvider.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace ServicesProvider.Models
{
    public class CategoryViewModel
    {
        [Required]
        public required int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Название' обязательно для заполнения")]
        [Display(Name = "Название")]
        public required string Name { get; set; }
    }
}
