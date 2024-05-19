using Microsoft.AspNetCore.Mvc;
using Rectangles.Models;
using Rectangles.Services;

namespace Rectangles.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RectangleController : ControllerBase
    {
        private readonly ILogger<RectangleController> _logger;
        private readonly IIntersectionService _intersectionService;

        public RectangleController(ILogger<RectangleController> logger, IIntersectionService intersectionService)
        {
            _logger = logger;
            _intersectionService = intersectionService;
        }

        //sides of rectangle should be in range 10
        [HttpGet("Get")]
        public async Task<IEnumerable<Rectangle>> Get()
        {
            return await _intersectionService.GetRectangles();
        }

        [HttpGet("GenerateRectangles")]
        public async Task<IEnumerable<Rectangle>> GenerateRectanglesAsync()
        {
            return await _intersectionService.GenerateRectanglesAsync();
        }

        [HttpGet("Intersect")]
        public async Task<List<Rectangle>> GetIntersectingRectanglesAsync(double x1, double y1, double x2, double y2)
        {
            var rectangles = await _intersectionService.GetRectangles();
            var segment = new Segment
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2
            };
            var intersectingRectangles = rectangles
                .Where(rect => _intersectionService.DoesIntersect(segment, rect))
                .ToList();

            return intersectingRectangles;
        }
    }
}