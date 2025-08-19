using System.ComponentModel.DataAnnotations;

namespace MyFirstWebAPIProject.Database.Models.Entities;

public class UserAddress
{
    public Guid Id { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    //1:1 relationship
    public Guid UserId { get; set; }
    public User User { get; set; } 
}