using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPIProject.Database.Models.Entities;
using MyFirstWebAPIProject.Dto;
using System.Transactions;
using WebAPITrainingProject.CommonUtils;
using WebAPITrainingProject.Services.Interfaces;

namespace WebAPITrainingProject.Services.Implementation;

public sealed class UserAddressService : IUserAddressService
{
    private readonly ICommonOperations<UserAddressService> _commonOper;
    public UserAddressService(ICommonOperations<UserAddressService> commonOperations)
    {
        _commonOper = commonOperations;
    }

    public async Task<Result<UserAddressGetDto>> CreateUserAddressesAsync(UserAddressCreateDto userAddressCreateDto)
    {
        try
        {
            var user = await _commonOper.DbContext.Users.FindAsync(userAddressCreateDto.UserId);

            if (user == null)
            {
                _commonOper.Logger.LogError("UserNotFound");
                return Result.Fail("UserNotFound");
            }

            if(user.UserAddress != null)
            {
                _commonOper.Logger.LogError("UserAddressExist");
                return Result.Fail("UserAddressExist");
            }

            var userAddressToCreate = _commonOper.Mapper.Map<UserAddress>(userAddressCreateDto);
            userAddressToCreate.User = user;

            var createdUserAddress = await _commonOper.DbContext.UsersAddresses.AddAsync(userAddressToCreate);

            if (createdUserAddress == null)
            {
                _commonOper.Logger.LogError("Cannot create user address");
                return Result.Fail("Cannot create user address");
            }

            var mappedUserAddress = _commonOper.Mapper.Map<UserAddressGetDto>(createdUserAddress.Entity);

            await _commonOper.DbContext.SaveChangesAsync();
            return Result.Ok(mappedUserAddress);
        }
        catch (AutoMapperMappingException ex)
        {
            _commonOper.Logger.LogError(ex, "B³¹d mapowania");

            return Result.Fail(new Error("B³¹d mapowania")
                         .WithMetadata("ErrorCode",
                         new ErrorInfo { ErrorCause = ErrorCause.eMappingErr, ErrorMessage = "ProblemWithMapping"}));
        }
        catch (Exception ex)
        {
            _commonOper.Logger.LogError(ex, "Cannot to create to UserAddress");
            return Result.Fail("Cannot to create to UserAddress");
        }
    }

    public Task<UserAddressGetDto> GetAddressByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}