using Microsoft.EntityFrameworkCore;

namespace Rectangles.Models
{
    public class Rectangle
    {
        public int Id { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}