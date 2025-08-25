using FluentResults;
using MyFirstWebAPIProject.Dto;

namespace WebAPITrainingProject.Services.Interfaces;

public interface IUserAddressService
{
    Task<Result<UserAddressGetDto>> CreateUserAddressesAsync(UserAddressCreateDto userAddressCreateDto);
    Task<UserAddressGetDto> GetAddressByUserIdAsync(Guid userId);
    Task<Result<UserAddressGetDto>> UpdateAddressByUserIdAsync(Guid userId, UserAddressUpdateDto userAddressUpdateDto);
}