using Microsoft.EntityFrameworkCore;

public class RectanglesDbContext : DbContext
{
    public RectanglesDbContext(DbContextOptions<RectanglesDbContext> options) : base(options)
    {
    }

    public DbSet<Rectangle> Rectangles { get; set; }
}

public class Rectangle
{
    public int Id { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}