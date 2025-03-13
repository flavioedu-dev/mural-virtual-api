namespace MuralVirtual.Domain.DTOs.Auth;

public class RegisterDTO
{
    public string? FullName { get; set; }
    public string? User { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmationPassword { get; set; }
}
