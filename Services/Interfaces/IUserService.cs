using FluentResults;
using MyFirstWebAPIProject.Dto;

namespace WebAPITrainingProject.Services.Interfaces;

public interface IUserService
{
    Task<Result<Guid>> CreateUser(UserCreateDto userCreateDto);
    Task<Result<UserGetDto>> GetUserByIdAsync(Guid id);
    Task<Result<UserGetDto>> UpdateUserAsync(Guid userId, UserUpdateDto userUpdateDto);
}
