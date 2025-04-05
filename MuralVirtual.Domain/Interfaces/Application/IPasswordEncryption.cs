namespace MuralVirtual.Domain.Interfaces.Application;

public interface IPasswordEncryption
{
    string HashPassword(string password);

    bool ComparePassword(string password, string hashDB);
}