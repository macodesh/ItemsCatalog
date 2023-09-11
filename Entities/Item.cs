namespace Catalog.Entities;
public record Item
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public decimal Price { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}
