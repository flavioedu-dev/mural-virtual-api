using AutoMapper;
using MuralVirtual.Application.Resources;
using MuralVirtual.Domain.DTOs.Auth;
using MuralVirtual.Domain.Entities;
using MuralVirtual.Domain.Interfaces.Application;
using MuralVirtual.Domain.Interfaces.Infrastructure.Repositories;

namespace MuralVirtual.Application.Services;

public class AuthServices : IAuthServices
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uof;

    public AuthServices(IMapper mapper, IUnitOfWork uof)
    {
        _mapper = mapper;
        _uof = uof;
    }

    public async Task<RegisterResponseDTO> Register(RegisterDTO registerDTO)
    {
        User user = _mapper.Map<User>(registerDTO);

        await _uof.UserRepository.CreateAsync(user);
        await _uof.CommitAsync();

        RegisterResponseDTO registerResponseDTO = _mapper.Map<RegisterResponseDTO>(ValueTuple.Create(user, ApplicationMessages.Auth_User_Register_Sucess));

        return registerResponseDTO;
    }
}
