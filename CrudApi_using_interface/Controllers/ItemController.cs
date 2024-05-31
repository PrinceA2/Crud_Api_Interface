using CrudApi_using_interface.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi_using_interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly ItemInterface _Interface;

        public ItemController(ItemInterface itemInterface)
        {
            _Interface = itemInterface;
        }

        [Route("GetallItem")]
        [HttpGet]

        public IActionResult GetallItem()
        {

            var items = _Interface.GetallItem();

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<ItemClass> GetItem(int id)
        {
            var item = _Interface.Get_specific_item(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Item/5
        [HttpPut("{id}")]
        public IActionResult PutItem(int id, ItemClass item)
        {
            if (id != item.ID)
            {
                return BadRequest();
            }

            var updatedItem = _Interface.Put_item(id, item);

            if (updatedItem == null)
            {
                return NotFound();
            }

            return Ok(updatedItem);
        }

        // POST: api/Item
        [HttpPost]
        public ActionResult<ItemClass> PostItem(ItemClass item)
        {
            var newItem = _Interface.Post_item(item);
            return CreatedAtAction(nameof(GetItem), new { id = newItem.ID }, newItem);
        }

        // DELETE: api/Item
        [HttpDelete]
        public IActionResult DeleteAllItems()
        {
            _Interface.Deleteall_Item();
            return NoContent();
        }

    }
}
