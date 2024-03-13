using Domain.Primitives;

namespace Domain.Categories;

public class Category : AggregateRoot<CategoryId>
{
    private Category(CategoryId id, string name) : base(id)
    {
        Name = name;
    }

    private Category()
    {
    }
    public string Name { get; private set; }

    public static Category Create(string name)
    {
        var category = new Category(new CategoryId(Guid.NewGuid()), name);

        return category;
    }
}
