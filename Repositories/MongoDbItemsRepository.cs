using Catalog.Entities;
using Catalog.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories;
public class MongoDbItemsRepository : IItemsRepository
{
    private const string DatabaseName = "Catalog";
    private const string CollectionName = "Items";
    private readonly IMongoCollection<Item> _itemsCollection;
    private readonly FilterDefinitionBuilder<Item> _filterBuilder = Builders<Item>.Filter;

    public MongoDbItemsRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(DatabaseName);
        _itemsCollection = database.GetCollection<Item>(CollectionName);
    }

    public async Task CreateItemAsync(Item item)
    {
        await _itemsCollection.InsertOneAsync(item);
    }

    public async Task DeleteItemAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(i => i.Id, id);
        await _itemsCollection.DeleteOneAsync(filter);
    }

    public async Task<Item?> GetItemAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(i => i.Id, id);
        return await _itemsCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
        return await _itemsCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateItemAsync(Item item)
    {
        var filter = _filterBuilder.Eq(i => i.Id, item.Id);
        await _itemsCollection.ReplaceOneAsync(filter, item);
    }
}
