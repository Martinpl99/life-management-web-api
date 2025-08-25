using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPIProject.Dto;
using WebAPITrainingProject.Services.Interfaces;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(UserCreateDto userCreateDto)
        {
            var result = await _userService.CreateUser(userCreateDto);

            return result.IsSuccess
            ? Ok(userCreateDto)
            : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDto>> GetUserByIdAsync([FromRoute] Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest();
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<UserGetDto>> UpdateUserAsync(Guid userId, UserUpdateDto userUpdateDto)
        {
            var result = await _userService.UpdateUserAsync(userId, userUpdateDto);

            return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest();
        }
    }
}
