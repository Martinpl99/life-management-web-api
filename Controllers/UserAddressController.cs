using FluentResults;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPIProject.Dto;
using WebAPITrainingProject.CommonUtils;
using WebAPITrainingProject.Services.Interfaces;

namespace MyApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UserAddressController : ControllerBase
{
    private readonly IUserAddressService _userAddressService;
    public UserAddressController(IUserAddressService userAddressService)
    {
        _userAddressService = userAddressService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAddressAsync(UserAddressCreateDto userAddressCreateDto)
    {
        var result = await _userAddressService.CreateUserAddressesAsync(userAddressCreateDto);

        if(result.IsFailed)
        {
            var error = result.Errors.FirstOrDefault();

            if (error is not null && error.Metadata.TryGetValue("ErrorCode", out var code))
                if (code is ErrorInfo errInfo && errInfo.ErrorCause == ErrorCause.eMappingErr)
                    return StatusCode(500, errInfo.ErrorMessage);

            return BadRequest();
        }

        return Ok(result.Value);
    }
}