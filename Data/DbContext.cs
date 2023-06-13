using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class ChooniContext : DbContext
{
    public string DbPath { get; }
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    // Other DbSet properties...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Tops" },
            new Category { Id = 2, Name = "Bottoms" },
            new Category { Id = 3, Name = "Accessories" }
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