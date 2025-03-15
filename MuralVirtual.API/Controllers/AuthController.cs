using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MuralVirtual.API.Models.Auth;
using MuralVirtual.Domain.DTOs.Auth;
using MuralVirtual.Domain.Interfaces.Application;

namespace MuralVirtual.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthServices _authServices;

    public AuthController(IMapper mapper, IAuthServices authServices)
    {
        _mapper = mapper;
        _authServices = authServices;
    }

    [HttpPost]
    public async Task<ActionResult<RegisterResponseDTO>> Register(RegisterModel registerModel)
    {
        RegisterDTO registerDTO = _mapper.Map<RegisterDTO>(registerModel);

        RegisterResponseDTO registerResponseDTO = await _authServices.Register(registerDTO);

        return Ok(registerResponseDTO);
    }
}