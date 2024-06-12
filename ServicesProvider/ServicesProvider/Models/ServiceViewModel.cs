using ServicesProvider.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace ServicesProvider.Models
{
    public class ServiceViewModel
    {
        [Required]
        public required int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Название' обязательно для заполнения")]
        [Display(Name = "Название")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Описание' обязательно для заполнения")]
        [Display(Name = "Описание")]
        public string Description { get; set; } = string.Empty;

        [RegularExpression(@"^\d+(.\d+)?$", ErrorMessage = "Цена должна содержать только цифры и точку")]
        [Required(ErrorMessage = "Поле 'Цена' обязательно для заполнения")]
        [Display(Name = "Цена")]
        public required decimal Price { get; set; }

        [Required(ErrorMessage = "Поле 'Категория' обязательно для заполнения")]
        [Display(Name = "Категория")]
        public required string Category { get; set; }
    }
}
