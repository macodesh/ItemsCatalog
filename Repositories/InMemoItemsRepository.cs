using Catalog.Entities;
using Catalog.Interfaces;

namespace Catalog.Repositories;
public class InMemoItemsRepository : IItemsRepository
{
    private readonly List<Item> items = new()
    {
        new Item { Id = Guid.Parse("0e503a46-bb3d-4f3b-b01b-cd2665f7c12d"), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
        new Item { Id = Guid.Parse("2841f26b-aa92-42e1-a86a-1351c6d23d1f"), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
        new Item { Id = Guid.Parse("501406d6-c810-4bc3-b172-6ce1f311b502"), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
    };

    public IEnumerable<Item> GetItems()
    {
        return items;
    }

    public Item? GetItem(Guid id)
    {
        return items.SingleOrDefault(i => i.Id == id);
    }
}
