using MediatR;

namespace CatalogService.Application.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name, Guid? ParentId) : IRequest<Guid>;
}
