using Microsoft.Extensions.Options;
using MuralVirtual.Domain.Configurations;
using MuralVirtual.Domain.Interfaces.Application;
using System.Security.Cryptography;

namespace MuralVirtual.Application.Services;

public class PasswordEncryption : IPasswordEncryption
{
    private int _iterations;

    public PasswordEncryption(IOptions<PasswordEncryptionOptions> _passwordEncryptionOptions)
    {
        _iterations = _passwordEncryptionOptions.Value.Iterations;
    }

    public string HashPassword(string password)
    {
        byte[] salt = new byte[16];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(salt);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterations);

        byte[] hash = pbkdf2.GetBytes(20);
        byte[] hashBytes = new byte[36];

        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        return Convert.ToBase64String(hashBytes);
    }

    public bool ComparePassword(string password, string hashDB)
    {
        byte[] hashBytes = Convert.FromBase64String(hashDB);

        byte[] salt = new byte[16];
        byte[] previusHash = new byte[20];

        Array.Copy(hashBytes, 0, salt, 0, 16);
        Array.Copy(hashBytes, 16, previusHash, 0, 20);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterations);

        byte[] newHash = pbkdf2.GetBytes(20);

        for (int i = 0; i < 20; i++)
        {
            if (previusHash[i] != newHash[i])
                return false;
        }

        return true;
    }
}