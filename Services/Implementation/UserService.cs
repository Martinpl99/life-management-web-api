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
    public UserService(ICommonOperations<UserService> commonOperations)
    {
        _commonOperations = commonOperations;
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

    public async Task<Result<UserGetDto>> GetUserById(Guid id)
    {
        try
        {
            var user = await _commonOperations.DbContext.Users.FirstAsync(x => x.Id == id);

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
}

