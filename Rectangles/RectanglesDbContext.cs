using Microsoft.EntityFrameworkCore;
using Rectangles.Models;

public class RectanglesDbContext : DbContext
{
    public RectanglesDbContext(DbContextOptions<RectanglesDbContext> options) : base(options)
    {
    }

    public DbSet<Rectangle> Rectangles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Rectangle>()
            .HasIndex(r => r.Width);

        modelBuilder.Entity<Rectangle>()
            .HasIndex(r => r.Height);
    }
}