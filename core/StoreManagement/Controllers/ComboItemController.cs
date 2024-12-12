using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;
using StoreManagement.Application.DTOs.Request.ComboItem;
using StoreManagement.Application.Interfaces.IServices;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboItemController : ControllerBase
    {
        private readonly IComboItemService _comboItemService;

        public ComboItemController(IComboItemService comboItemService)
        {
            this._comboItemService = comboItemService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateComboItemByListReq req)
        {
            var list = await _comboItemService.CreateByListIdFood(req.IdCombo, req.ListIdFood);
            return Ok(Result<List<ComboItemDTO>>.Success(list, "Get list success"));
        }

        // Get ComboItems by ComboId
        [HttpGet("by-combo/{comboId}")]
        public async Task<IActionResult> GetByComboId(int comboId)
        {
            var comboItems = await _comboItemService.GetByComboIdAsync(comboId);
            if (comboItems == null || comboItems.Count == 0)
                return NotFound(Result<string>.Failure("No combo items found for the given combo ID"));

            return Ok(Result<List<ComboItemDTO>>.Success(comboItems, "Get combo items successfully"));
        }

        // Update ComboItem
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ComboItemDTO comboItemDTO)
        {
            var updatedComboItem = await _comboItemService.UpdateAsync(id, comboItemDTO);
            return Ok(Result<ComboItemDTO>.Success(updatedComboItem, "Update combo item successfully"));
        }

        // Delete ComboItem
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _comboItemService.DeleteAsync(id);
            if (!success)
                return NotFound(Result<string>.Failure("Combo item not found"));

            return Ok(Result<string>.Success("Delete combo item successfully"));
        }

    }
}
