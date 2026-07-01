using CatalogService.Application.Common.DTOs;
using CatalogService.Domain.Repositories;
using MediatR;

namespace CatalogService.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDTO>>
    {
        private readonly ICategoryRepository _repository;

        public GetAllCategoriesHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryDTO>> Handle(GetAllCategoriesQuery request, CancellationToken ct)
        {
            var categories = await _repository.GetAllAsync(ct);
            return categories.Select(c => new CategoryDTO(c.Id, c.Name)).ToList();
        }
    }
}
