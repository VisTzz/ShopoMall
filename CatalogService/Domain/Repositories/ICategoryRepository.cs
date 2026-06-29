using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken ct);
        Task AddAsync(Category category, CancellationToken ct);

        Task DeleteAsync(Category category, CancellationToken ct);
        Task UpdateAsync(Category category, CancellationToken ct);

        Task<List<Category>> GetAllAsync(CancellationToken ct);
    }
}
