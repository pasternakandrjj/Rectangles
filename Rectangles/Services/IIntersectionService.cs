using Rectangles.Models;

namespace Rectangles.Services
{
    public interface IIntersectionService
    {
        Task<IEnumerable<Rectangle>> GetRectangles();
        Task<IEnumerable<Rectangle>> GenerateRectanglesAsync();
        bool DoesIntersect(Segment segment, Rectangle rectangle);

    }
}
