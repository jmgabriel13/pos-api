namespace Domain.Products;

// Stock Keeping Unit
public record Sku
{
    private const int DefaultLength = 15;

    // use this private constructor from create factory method
    // to be able to initialize a new sku instance
    private Sku(string value) => Value = value;

    public string Value { get; init; }

    // factory method that create Sku instance
    public static Sku? Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        if (value.Length != DefaultLength)
        {
            return null;
        }

        return new Sku(value);
    }
}