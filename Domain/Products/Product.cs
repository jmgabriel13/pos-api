namespace Domain.Products;
public class Product
{
    public ProductId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    // value objects, to encapsulate some complex types
    // objects defined by their value
    // some sort of validations built in to them
    public Money Price { get; private set; }
    public Sku Sku { get; private set; }
}