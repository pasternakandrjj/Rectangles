using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Rectangles.Models;
using Rectangles.Services;

namespace Rectangles.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RectangleController : ControllerBase
    {
        private readonly ILogger<RectangleController> _logger;
        private readonly IRectangleService _rectangleService;

        public RectangleController(ILogger<RectangleController> logger, IRectangleService rectangleService)
        {
            _logger = logger;
            _rectangleService = rectangleService;
        }

        //sides of rectangle should be in range 10
        [HttpGet("Get")]
        public async Task<IEnumerable<Rectangle>> Get(double a, double b)
        {
            return await _rectangleService.GetRectangles(a, b);
        }

        [HttpGet("GenerateRectangles")]
        public async Task<IEnumerable<Rectangle>> GenerateRectanglesAsync()
        {
            return await _rectangleService.GenerateRectanglesAsync();
        }
    }
}