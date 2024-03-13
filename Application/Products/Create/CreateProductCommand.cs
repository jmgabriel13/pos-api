using Application.Interfaces;

namespace Application.Products.Create;
public record CreateProductCommand(
        string Name,
        string Description,
        decimal Price,
        string Sku,
        int Stocks,
        List<string> Categories,
        List<string> Sizes,
        List<string> SideDishes) : ICommand;
