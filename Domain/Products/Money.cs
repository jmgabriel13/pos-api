namespace Domain.Products;

public record Money(string Currency = "PHP", decimal Amount = 0);
