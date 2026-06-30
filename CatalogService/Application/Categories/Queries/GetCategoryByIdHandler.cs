using CatalogService.Domain.Repositories;

namespace CatalogService.Application.Categories.Queries
{
    public class GetCategoryByIdHandler
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryByIdHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDTO> Handle (GetCategoryByIdQuery query, CancellationToken ct)
        {
            var category = await _repository.GetCategoryByIdAsync(query.Id, ct);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with {query.Id} does not exist");
            }

                return new CategoryDTO(category.Name, category.Id);
        }

        public record CategoryDTO(string Name, Guid Id);
    }
}
