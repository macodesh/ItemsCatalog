using Catalog.Entities;

namespace Catalog.Interfaces;
public interface IItemsRepository
{
    Item? GetItem(Guid id);
    IEnumerable<Item> GetItems();
}
