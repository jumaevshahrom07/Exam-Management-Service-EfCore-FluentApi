using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required, MinLength(3), MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    // navigation properties
    public List<Product> Products { get; set; } = [];
}