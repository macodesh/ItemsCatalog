using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;
[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemsRepository _repository;

    public ItemsController(IItemsRepository repository)
    {
        _repository = repository;
    }

    // Get /items

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsAsync()
    {
        var items = (await _repository.GetItemsAsync())
            .Select(i => i.AsDto());

        return Ok(items);
    }

    // Get /items/0e503a46-bb3d-4f3b-b01b-cd2665f7c12d

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
    {
        var item = await _repository.GetItemAsync(id);
        if (item is null) return NotFound();
        return Ok(item.AsDto());
    }

    // Post /items

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync([FromBody] CreateItemDto itemDto)
    {
        Item item = new()
        {
            Id = Guid.NewGuid(),
            Name = itemDto.Name,
            Price = itemDto.Price,
            CreatedDate = DateTimeOffset.Now
        };

        await _repository.CreateItemAsync(item);
        return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
    }

    // Put /items/0e503a46-bb3d-4f3b-b01b-cd2665f7c12d

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, [FromBody] UpdateItemDto itemDto)
    {
        var existingItem = await _repository.GetItemAsync(id);
        if (existingItem is null) return NotFound();

        Item updatedItem = existingItem with
        {
            Name = itemDto.Name,
            Price = itemDto.Price
        };

        await _repository.UpdateItemAsync(updatedItem);
        return NoContent();
    }

    // Delete /items/0e503a46-bb3d-4f3b-b01b-cd2665f7c12d

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItemAsync(Guid id)
    {
        var existingItem = await _repository.GetItemAsync(id);
        if (existingItem is null) return NotFound();

        await _repository.DeleteItemAsync(id);
        return NoContent();
    }
}
