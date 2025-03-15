using MuralVirtual.Domain.DTOs.Auth;

namespace MuralVirtual.Domain.Interfaces.Application;

public interface IAuthServices
{
    Task<RegisterResponseDTO> Register(RegisterDTO registerDTO);
}
