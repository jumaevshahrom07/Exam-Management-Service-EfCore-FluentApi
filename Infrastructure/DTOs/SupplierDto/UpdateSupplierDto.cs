using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTOs.SupplierDto;

public class UpdateSupplierDto
{
    [Required(ErrorMessage = "Supplier name is required")]
    [MinLength(3), MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    [MinLength(9), MaxLength(20)]
    public string Phone { get; set; } = string.Empty;
}