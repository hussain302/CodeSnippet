﻿using CodeSnippet.Application.Abstractions.MediatR;
using CodeSnippet.Application.Dtos;
using CodeSnippet.Domain.Entities;
using CodeSnippet.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace CodeSnippet.Application.Modules.Users.Commands;

public partial record UpdateUserCommand(Guid Id, string Username, string FirstName, string MiddleName, string LastName, string Password, string Email, RoleDto Role, Address Address) : ICommand<bool>
{
    public bool IsValidUser => PerformUserValidation();
    public bool IsValidEmail => IsEmailValid();
    public bool IsValidPassword => IsPasswordStrong();

    private bool PerformUserValidation()
        => !(string.IsNullOrWhiteSpace(Username) ||
             string.IsNullOrWhiteSpace(FirstName) ||
             string.IsNullOrWhiteSpace(LastName) ||
             string.IsNullOrWhiteSpace(Password) ||
             string.IsNullOrWhiteSpace(Email) ||
             Role.Id == Guid.Empty);

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex MyRegex();
    private bool IsEmailValid()
        => MyRegex().IsMatch(Email);

    private bool IsPasswordStrong()
        => Password.Length >= 8 &&
               Password.Any(char.IsUpper) &&
               Password.Any(char.IsLower) &&
               Password.Any(char.IsDigit);
}