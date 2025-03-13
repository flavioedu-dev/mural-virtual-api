using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MuralVirtual.API.Models.Auth;
using MuralVirtual.Domain.DTOs.Auth;

namespace MuralVirtual.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;

    public AuthController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public ActionResult<RegisterResponseDTO> Register(RegisterModel registerModel)
    {
        RegisterDTO registerDTO = _mapper.Map<RegisterDTO>(registerModel);

        RegisterResponseDTO registerResponseDTO = new();

        return Ok(registerResponseDTO);
    }
}
