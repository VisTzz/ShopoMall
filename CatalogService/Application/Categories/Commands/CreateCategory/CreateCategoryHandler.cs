using CatalogService.Domain.Entities;
using CatalogService.Domain.Repositories;
using CatalogService.Infrastructure.Data;
using MediatR;

namespace CatalogService.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;


        public CreateCategoryHandler(ICategoryRepository categoryRepository, ApplicationDbContext context)
        {
            _categoryRepository = categoryRepository;
            _context = context;
        }

        public async Task<Guid> Handle(CreateCategoryCommand command, CancellationToken ct)
        {
            var category = new Category(command.Name, command.ParentId);

            await _categoryRepository.AddAsync(category, ct);
            await _context.SaveChangesAsync(ct);

            return category.Id;
        }

    }
}
