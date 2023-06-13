using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public Category Category { get; set; }

    [Required]
    public Type Type { get; set; }
    public ICollection<Color> Colors { get; set; }

    [Required]
    public ICollection<Size> Sizes { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Summary { get; set; }

    [Required]
    public int Stock { get; set; }

    [Required]
    public ICollection<Picture> Pictures { get; set; }
}