using AutoMapper;
using MyFirstWebAPIProject.Database.Models.Entities;
using MyFirstWebAPIProject.Dto;

namespace WebAPITrainingProject.Automapper;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        //User
        CreateMap<UserCreateDto, User>().ReverseMap();
        CreateMap<UserGetDto, User>().ReverseMap();
        CreateMap<UserUpdateDto, User>().ReverseMap();

        //UserAddress
        CreateMap<UserAddressCreateDto, UserAddress>().ReverseMap();
        CreateMap<UserAddress, UserAddressGetDto>().ReverseMap();
        CreateMap<UserAddress, UserAddressUpdateDto>().ReverseMap();
    }
}
