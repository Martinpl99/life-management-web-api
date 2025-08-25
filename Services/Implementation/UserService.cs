using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPIProject.Database.DatabaseContext;
using MyFirstWebAPIProject.Database.Models.Entities;
using MyFirstWebAPIProject.Dto;
using WebAPITrainingProject.CommonUtils;
using WebAPITrainingProject.Services.Interfaces;

namespace WebAPITrainingProject.Services.Implementation;

public class UserService : IUserService
{
    private readonly ICommonOperations<UserService> _commonOperations;
    private readonly IUserAddressService _userAddressService;
    public UserService(ICommonOperations<UserService> commonOperations,
        IUserAddressService userAddressService)
    {
        _commonOperations = commonOperations;
        _userAddressService = userAddressService;
    }

    public async Task<Result<Guid>> CreateUser(UserCreateDto userCreateDto)
    {
        try
        {
            var user = _commonOperations.Mapper.Map<User>(userCreateDto);
            await _commonOperations.DbContext.Users.AddAsync(_commonOperations.Mapper.Map<User>(userCreateDto));
            await _commonOperations.DbContext.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            _commonOperations.Logger.LogError(ex, "Cannot create User");
            return Result.Fail("Cannot create User");
        }
    }

    public async Task<Result<UserGetDto>> GetUserByIdAsync(Guid id)
    {
        try
        {
            var user = await _commonOperations.DbContext.Users
                .Include(u => u.UserAddress)
                .FirstAsync(x => x.Id == id);

            if (user == null)
                return Result.Fail("Wrong given Id");

            return Result.Ok(_commonOperations.Mapper.Map<UserGetDto>(user));
        }
        catch (Exception ex)
        {
            _commonOperations.Logger.LogError(ex, "Cannot get User by Id");
            return Result.Fail("Cannot find user by specified id");
        }
    }

    public async Task<Result<UserGetDto>> UpdateUserAsync(Guid userId, UserUpdateDto userUpdateDto)
    {
        using var transaction = await _commonOperations.DbContext.Database.BeginTransactionAsync();

        if (userUpdateDto == null) return Result.Fail("Incoming user data is empty");

        try
        {
            if (userId == Guid.Empty) return Result.Fail("User id is empty");

            var user = await _commonOperations.DbContext.Users
                .FirstAsync(x => x.Id == userId);

            _commonOperations.Mapper.Map(userUpdateDto, user);

            if (userUpdateDto.UserAddress is not null)
            {
                var addressResult =
                    await _userAddressService.UpdateAddressByUserIdAsync(user.Id, userUpdateDto.UserAddress);

                if (addressResult.IsFailed)
                {
                    await transaction.RollbackAsync();
                    return Result.Fail("Cannot update user data");
                }
            }

            var userGetDto = _commonOperations.Mapper.Map<UserGetDto>(user);

            await transaction.CommitAsync();
            return Result.Ok(userGetDto);

        }
        catch(Exception ex)
        {
            _commonOperations.Logger.LogError(ex, "Cannot update user data");
            await transaction.RollbackAsync();
            return Result.Fail("Cannot update user data");
        }
    }
}

