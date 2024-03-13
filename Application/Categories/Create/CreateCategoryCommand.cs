using Application.Interfaces;

namespace Application.Categories.Create;
public record CreateCategoryCommand(string Name) : ICommand;
