using StoreManagement.Application.DTOs;
using StoreManagement.Application.Interfaces.IServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Common;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
            
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Result>> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return BadRequest(Result.Failure("Không tìm thấy người dùng"));
            }
            return Ok(Result<UserDTO?>.Success(user, "Lấy thông tin thành công"));
        }

        [HttpPut("update")]
        public async Task<ActionResult<Result>> Update(UserDTO user)
        {
            var update = await _userService.Edit(user);
            if (update == null)
            {
                return BadRequest(Result.Failure("Không tìm thấy người dùng"));
            }
            return Ok(Result<UserDTO?>.Success(update, "Cập nhật thông tin thành công"));
        }

    }
}
