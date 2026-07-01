using CatalogService.Domain.Entities;
using CatalogService.Domain.Repositories;
using CatalogService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddAsync(Category category, CancellationToken ct)
        {
            await _context.Categories.AddAsync(category, ct);
        }

        public async Task UpdateAsync(Category category, CancellationToken ct)
        {
            _context.Categories.Update(category);
        }

        public async Task DeleteAsync(Category category, CancellationToken ct)
        {
            _context.Categories.Remove(category);
        }

        public async Task<List<Category>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Categories.ToListAsync(ct);
        }
    }
}
