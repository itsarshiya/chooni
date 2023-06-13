using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

public class Picture
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

      [Required]
    public string AltText { get; set; }

      [Required]
    public string Url { get; set; }
}

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

      [Required]
    public string Name { get; set; }
}

public class Type
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

      [Required]
    public string Name { get; set; }

      [Required]
    public Category Category { get; set; }
}


public class Size
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

      [Required]
    public string Name { get; set; }
}

public class Color
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
    [Required]
    public string Hex { get; set; }

}