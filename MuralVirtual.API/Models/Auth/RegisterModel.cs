﻿namespace MuralVirtual.API.Models.Auth;

public class RegisterModel
{
    public string? FullName { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmationPassword { get; set; }
}