using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Rectangles.Models;

namespace Rectangles.Services
{
    public class RectangleService : IRectangleService
    {
        private readonly RectanglesDbContext _context;
        private readonly IMemoryCache _cache;

        public RectangleService(RectanglesDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<Rectangle>> GetRectangles(double a, double b)
        {
            var cacheKey = $"rectangles_{a}_{b}";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Rectangle> result))
            {
                result = await _context.Rectangles
                    .Where(x => (x.Width == a || x.Height == a) && (x.Width == b || x.Height == b))
                    .ToListAsync();

                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5)); // Cache for 5 minutes
            }

            return result;
        }

        public async Task<IEnumerable<Rectangle>> GenerateRectanglesAsync()
        {
            List<Rectangle> result = new List<Rectangle>();
            Random random = new Random();
            const int count = 25;

            for (int i = 0; i < count; i++)
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

            // Batch insertion and transaction
            //using (var transaction = await _context.Database.BeginTransactionAsync())
            //{
            //    try
            //    {
            //        await _context.Rectangles.AddRangeAsync(result);
            //        await _context.SaveChangesAsync();
            //        await transaction.CommitAsync();
            //    }
            //    catch (Exception)
            //    {
            //        await transaction.RollbackAsync();
            //        throw;
            //    }
            //}

            return result;
        }
    }
}
