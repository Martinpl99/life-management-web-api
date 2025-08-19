namespace MyFirstWebAPIProject.Database.Models.Entities;

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public UserAddress? UserAddress { get; set; }
}