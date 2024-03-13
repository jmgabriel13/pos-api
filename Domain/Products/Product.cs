using Domain.Primitives;

namespace Domain.Products;
public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    // value objects, to encapsulate some complex types
    // objects defined by their value
    // some sort of validations built in to them
    public Money Price { get; private set; }
    public Sku Sku { get; private set; }
    public int Stocks { get; private set; }
    public List<string> Categories { get; private set; }
    public List<string> Sizes { get; private set; }
    public List<string> SideDishes { get; private set; }

    private Product()
    {
    }

    private Product(
        ProductId id,
        string name,
        string description,
        Money price,
        Sku sku,
        int stocks,
        List<string> categories,
        List<string> sizes,
        List<string> sideDishes) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        Sku = sku;
        Stocks = stocks;
        Categories = categories;
        Sizes = sizes;
        SideDishes = sideDishes;
    }

    public static Product Create(
        string name,
        string description,
        Money price,
        Sku sku,
        int stocks,
        List<string> categories,
        List<string> sizes,
        List<string> sideDishes)
    {
        var prod = new Product(
            new ProductId(Guid.NewGuid()),
            name,
            description,
            price,
            sku,
            stocks,
            categories,
            sizes,
            sideDishes);

        // raise an event


        return prod;
    }

}
