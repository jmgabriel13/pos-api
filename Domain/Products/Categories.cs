namespace Domain.Products;

public class Categories
{
    internal Categories(Guid id, string name, DateTime updatedAt)
    {
        Id = id;
        Name = name;
        UpdatedAt = updatedAt;
    }

    private Categories()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; }
}