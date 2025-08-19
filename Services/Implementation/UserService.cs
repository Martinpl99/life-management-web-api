using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPIProject.Database.DatabaseContext;
using MyFirstWebAPIProject.Database.Models.Entities;
using MyFirstWebAPIProject.Dto;
using WebAPITrainingProject.Services.Interfaces;

namespace WebAPITrainingProject.Services.Implementation;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    public UserService(IApplicationDbContext dbContext, ILogger<UserService> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> CreateUser(UserCreateDto userCreateDto)
    {
        try
        {
            var user = _mapper.Map<User>(userCreateDto);
            await _dbContext.Users.AddAsync(_mapper.Map<User>(userCreateDto));
            await _dbContext.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cannot create User");
            return Result.Fail("Cannot create User");
        }
    }

    public async Task<Result<UserGetDto>> GetUserById(Guid id)
    {
        try
        {
            var user = await _dbContext.Users.FirstAsync(x => x.Id == id);

            if (user == null)
                return Result.Fail("Wrong given Id");

            return Result.Ok(_mapper.Map<UserGetDto>(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cannot get User by Id");
            return Result.Fail("Cannot find user by specified id");
        }
    }
}

