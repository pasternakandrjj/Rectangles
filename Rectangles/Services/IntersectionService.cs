using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Rectangles.Models;

namespace Rectangles.Services
{
    public class IntersectionService : IIntersectionService
    {
        private readonly RectanglesDbContext _context;
        private readonly IMemoryCache _cache;

        public IntersectionService(RectanglesDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<Rectangle>> GetRectangles()
        {
            var cacheKey = $"rectangles";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Rectangle> result))
            {
                result = await _context.Rectangles
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
                    X = random.Next(0, 6),
                    Y = random.Next(0, 6),
                    Width = random.Next(1, 11),
                    Height = random.Next(1, 11)
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

        public bool DoesIntersect(Segment segment, Rectangle rectangle)
        {
            var edges = new[]
            {
                new Segment { X1 = rectangle.X, Y1 = rectangle.Y, X2 = rectangle.X + rectangle.Width, Y2 = rectangle.Y },
                new Segment { X1 = rectangle.X, Y1 = rectangle.Y, X2 = rectangle.X, Y2 = rectangle.Y + rectangle.Height },
                new Segment { X1 = rectangle.X + rectangle.Width, Y1 = rectangle.Y, X2 = rectangle.X + rectangle.Width, Y2 = rectangle.Y + rectangle.Height },
                new Segment { X1 = rectangle.X, Y1 = rectangle.Y + rectangle.Height, X2 = rectangle.X + rectangle.Width, Y2 = rectangle.Y + rectangle.Height }
            };

            foreach (var edge in edges)
            {
                if (SegmentsIntersect(segment, edge))
                {
                    return true;
                }
            }

            return false;
        }

        private bool SegmentsIntersect(Segment s1, Segment s2)
        {
            double dx1 = s1.X2 - s1.X1;
            double dy1 = s1.Y2 - s1.Y1;
            double dx2 = s2.X2 - s2.X1;
            double dy2 = s2.Y2 - s2.Y1;

            double determinant = (-dx2 * dy1 + dx1 * dy2);
            if (determinant == 0)
            {
                return false;
            }

            double s = (-dy1 * (s1.X1 - s2.X1) + dx1 * (s1.Y1 - s2.Y1)) / determinant;
            double t = (dx2 * (s1.Y1 - s2.Y1) - dy2 * (s1.X1 - s2.X1)) / determinant;

            return s >= 0 && s <= 1 && t >= 0 && t <= 1;
        }
    }
}