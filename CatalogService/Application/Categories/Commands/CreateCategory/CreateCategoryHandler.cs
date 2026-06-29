using CatalogService.Domain.Entities;
using CatalogService.Domain.Repositories;

namespace CatalogService.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryHandler
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(CreateCategoryCommand command, CancellationToken ct)
        {
            var category = new Category(command.Name, command.ParentId);

            await _categoryRepository.AddAsync(category, ct);

            return category.Id;
        }

    }
}
