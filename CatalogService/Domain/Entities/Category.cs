using CatalogService.Domain.Exceptions;

namespace CatalogService.Domain.Entities
{
    public enum CategoryStatus
    {
        Active,
        Disabled
    }
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid? ParentId { get; private set; }

        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        public CategoryStatus IsAvailable { get; private set; }

        public Category() { }

        public Category(string name, Guid? parentId)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new CategoryException("Name should be filled");
            if (parentId.HasValue && parentId.Value == Guid.Empty) throw new CategoryException("Parent ID should be  filled");

            Id = Guid.NewGuid();
            Name = name;
            ParentId = parentId;
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
            IsAvailable = CategoryStatus.Active;
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new CategoryException("Name should be filled");

            Name = newName.Trim();
            UpdatedOn = DateTime.UtcNow;
        }

    }
}
