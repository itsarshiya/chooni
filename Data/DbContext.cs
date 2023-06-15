using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class ChooniContext : DbContext
{
    public string DbPath { get; }
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Picture> Pictures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Tops" },
            new Category { Id = 2, Name = "Bottoms" },
            new Category { Id = 3, Name = "Accessories" }
        );

        // Seed Types
        modelBuilder.Entity<Type>().HasData(
            new Type { Id = 1, Name = "Type 1", CategoryId = 1 },
            new Type { Id = 2, Name = "Type 2", CategoryId = 2 },
            new Type { Id = 3, Name = "Type 3", CategoryId = 3 }
        );

        // Seed Sizes
        modelBuilder.Entity<Size>().HasData(
            new Size { Id = 1, Name = "Small" },
            new Size { Id = 2, Name = "Medium" },
            new Size { Id = 3, Name = "Large" }
        );

        // Seed Colors
        modelBuilder.Entity<Color>().HasData(
            new Color { Id = 1, Name = "Red", Hex = "#FF0000" },
            new Color { Id = 2, Name = "Green", Hex = "#00FF00" },
            new Color { Id = 3, Name = "Blue", Hex = "#0000FF" }
        );

        // Seed Pictures
        modelBuilder.Entity<Picture>().HasData(
            new Picture { Id = 1, AltText = "Picture 1", Url = "https://example.com/picture1.jpg" },
            new Picture { Id = 2, AltText = "Picture 2", Url = "https://example.com/picture2.jpg" },
            new Picture { Id = 3, AltText = "Picture 3", Url = "https://example.com/picture3.jpg" }
        );

    }
    public ChooniContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "chooni.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}