using CatalogService.Application.Common.DTOs;
using CatalogService.Domain.Repositories;
using MediatR;

namespace CatalogService.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDTO>
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryByIdHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDTO> Handle(GetCategoryByIdQuery query, CancellationToken ct)
        {
            var category = await _repository.GetCategoryByIdAsync(query.Id, ct);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with {query.Id} does not exist");
            }

            return new CategoryDTO(category.Id, category.Name);
        }
    }
}
