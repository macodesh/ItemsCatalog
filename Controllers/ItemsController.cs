using Catalog.Dtos;
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
    public ActionResult<IEnumerable<ItemDto>> GetItems()
    {
        var items = _repository.GetItems().Select(i => i.AsDto());
        return Ok(items);
    }

    // Get /items/0e503a46-bb3d-4f3b-b01b-cd2665f7c12d
    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItem(Guid id)
    {
        var item = _repository.GetItem(id);
        if (item is null) return NotFound();
        return Ok(item.AsDto());
    }
}
