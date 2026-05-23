using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTOs.CategoryDto;

public class UpdateCategoryDto
{
    [Required(ErrorMessage = "Category name is required")]
    [MinLength(3), MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}