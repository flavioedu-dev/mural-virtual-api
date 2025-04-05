using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using FluentAssertions;
using Moq;
using MuralVirtual.API.Mapping;
using MuralVirtual.Application.Services;
using MuralVirtual.Domain.DTOs.Auth;
using MuralVirtual.Domain.Entities;
using MuralVirtual.Domain.Exceptions;
using MuralVirtual.Domain.Interfaces.Application;
using MuralVirtual.Domain.Interfaces.Infrastructure.Repositories;
using System.Linq.Expressions;

namespace MuralVirtual.Application.Tests.Auth;

public class RegisterServiceTests : IDisposable
{
    private IAuthServices _authServicesMock;
    private IMapper _mapperMock;

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IPasswordEncryption> _passwordEncryptionMock;

    private RegisterDTO _registerDTOMock;

    public RegisterServiceTests()
    {
        _registerDTOMock = new()
        {
            FullName = "Jãozin Teste",
            Email = "jaozin@gmail.com",
            Username = "jaozin",
            Password = "Jao12345",
            ConfirmationPassword = "Jao12345",
        };

        _mapperMock = new MapperConfiguration(cfg => cfg.AddProfile(new AuthMapping())).CreateMapper();

        _unitOfWorkMock = new();
        _unitOfWorkMock
            .SetupSequence(x => x.UserRepository.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync((User?)null)
            .ReturnsAsync((User?)null);

        _unitOfWorkMock.Setup(x => x.UserRepository.CreateAsync(It.IsAny<User>()));
        _unitOfWorkMock.Setup(x => x.CommitAsync());

        _passwordEncryptionMock = new();
        _passwordEncryptionMock.Setup(x => x.HashPassword(It.IsAny<string>())).Returns("");

        _authServicesMock = new AuthServices(_mapperMock, _unitOfWorkMock.Object, _passwordEncryptionMock.Object);
    }

    public void Dispose()
    {
        _mapperMock = default!;
        _unitOfWorkMock = default!;
        _passwordEncryptionMock = default!;
        _authServicesMock = default!;
    }


    [Fact]
    public async void Register_WhenDataIsValid_ThenNotThrow()
    {
        // Assert
        await FluentActions
          .Invoking(() => _authServicesMock.Register(_registerDTOMock))
          .Should()
          .NotThrowAsync();
    }

    [Fact]
    public async void Register_WhenUsernameExists_ThenThrowUsernameExistsException()
    {
        // Arrange
        string errorMsg = "User already registered";

        _unitOfWorkMock
            .SetupSequence(x => x.UserRepository.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new User() { Id = 1 })
            .ReturnsAsync((User?)null);


        // Assert
        await FluentActions
            .Invoking(() => _authServicesMock.Register(_registerDTOMock))
            .Should()
            .ThrowExactlyAsync<CustomResponseException>()
            .WithMessage(errorMsg);
    }

    [Fact]
    public async void Register_WhenEmailExists_ThenThrowEmailExistsException()
    {
        // Arrange
        string errorMsg = "User already registered";

        _unitOfWorkMock
            .SetupSequence(x => x.UserRepository.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync((User?)null)
            .ReturnsAsync(new User() { Id = 1 });

        // Assert
        await FluentActions
            .Invoking(() => _authServicesMock.Register(_registerDTOMock))
            .Should()
            .ThrowExactlyAsync<CustomResponseException>()
            .WithMessage(errorMsg);
    }

    [Fact]
    public async void Register_WhenDataIsValid_ThenReturnRegisterResponseDTO()
    {
        // Arrange
        RegisterResponseDTO registerResponseDTOExpected = new()
        {
            Id = It.IsAny<long>(),
            Message = "User registered successfully"
        };

        // Act
        RegisterResponseDTO registerResponseDTO = await _authServicesMock.Register(_registerDTOMock);

        // Assert
        registerResponseDTO.Should().BeEquivalentTo(registerResponseDTOExpected);
    }

    [Fact]
    public async void Register_WhenUsernameExists_ThenGetAsync()
    {
        // Arrange
        _unitOfWorkMock.SetupSequence(x => x.UserRepository.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new User() { Id = 1 })
            .ReturnsAsync((User?)null);

        // Act
        try
        {
            await _authServicesMock.Register(_registerDTOMock);
        }
        catch
        {

        }

        // Assert
        _unitOfWorkMock.Verify(x => x.UserRepository.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
    }

    [Fact]
    public async void Register_WhenUsernameDoesNotExistButEmailExists_ThenTwoGetAsync()
    {
        // Arrange
        _unitOfWorkMock.SetupSequence(x => x.UserRepository.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync((User?)null)
            .ReturnsAsync(new User() { Id = 1 });

        // Act
        try
        {
            await _authServicesMock.Register(_registerDTOMock);
        }
        catch
        {

        }

        // Assert
        _unitOfWorkMock.Verify(x => x.UserRepository.GetAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Exactly(2));
    }

    [Fact]
    public async void Register_WhenDataIsValid_ThenHashPassword()
    {
        // Act
        await _authServicesMock.Register(_registerDTOMock);

        // Assert
        _passwordEncryptionMock.Verify(x => x.HashPassword(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async void Register_WhenDataIsValid_ThenCreateAsync()
    {
        // Act
        await _authServicesMock.Register(_registerDTOMock);

        // Assert
        _unitOfWorkMock.Verify(x => x.UserRepository.CreateAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async void Register_WhenDataIsValid_ThenCommitAsync()
    {
        // Act
        await _authServicesMock.Register(_registerDTOMock);

        // Assert
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }
}