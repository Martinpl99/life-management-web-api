namespace  MyFirstWebAPIProject.Dto;

public record class UserGetDto(string? Name, string? LastName, UserAddressGetDto? UserAddress);