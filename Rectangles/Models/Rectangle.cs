using Microsoft.EntityFrameworkCore;

namespace Rectangles.Models
{
    public class Rectangle
    {
        public int Id { get; set; }
         
        public int Width { get; set; }
         
        public int Height { get; set; }
    }
}