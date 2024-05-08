using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Rectangles.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RectangleController : ControllerBase
    {
        private readonly ILogger<RectangleController> _logger;
        private readonly RectanglesDbContext _context;

        public RectangleController(ILogger<RectangleController> logger, RectanglesDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //sides of rectangle should be in range 10
        [HttpGet("Get")]
        public async Task<IEnumerable<Rectangle>> Get(double a, double b)
        {
            return await _context.Rectangles.Where(x => x.Width == a || x.Height == a || x.Width == b || x.Height == b).ToListAsync();
        }

        [HttpGet(Name = "GenerateRectangles")]
        public async Task<IEnumerable<Rectangle>> GenerateRectanglesAsync()
        {
            List<Rectangle> result = new List<Rectangle>();
            Random random = new Random();
            for (int i = 0; i < 25; i++)
            {
                result.Add(new Rectangle()
                {
                    Height = random.Next(10),
                    Width = random.Next(10),
                }
                );
            }
            await _context.Rectangles.AddRangeAsync(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}