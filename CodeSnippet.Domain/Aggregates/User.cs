using CodeSnippet.Domain.Entities;
using CodeSnippet.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSnippet.Domain.Aggregates;

[Table("tblUsers")]
public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid RoleId { get; set; } //FK
    public Role Role { get; set; } //navigator
    public Address? Address { get; set; }

}
