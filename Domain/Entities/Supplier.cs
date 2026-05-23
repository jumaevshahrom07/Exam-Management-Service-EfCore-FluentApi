using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Supplier
{
    [Key]
    public int Id { get; set; }

    [Required, MinLength(3), MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required, MinLength(9), MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    // navigation properties
    public List<Product> Products { get; set; } = [];
}