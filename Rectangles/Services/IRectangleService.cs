using Rectangles.Models;

namespace Rectangles.Services
{
    public interface IRectangleService
    {
        Task<IEnumerable<Rectangle>> GetRectangles(double a, double b);
        Task<IEnumerable<Rectangle>> GenerateRectanglesAsync();
    }
}
