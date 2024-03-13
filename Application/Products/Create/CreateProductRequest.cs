namespace Application.Products.Create;
public record CreateProductRequest(
        string Name,
        string Description,
        decimal Price,
        string Sku,
        int Stocks,
        List<string> Categories,
        List<string> Sizes,
        List<string> SideDishes);
