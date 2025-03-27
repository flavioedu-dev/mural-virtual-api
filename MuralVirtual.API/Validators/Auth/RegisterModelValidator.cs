using FluentValidation;
using MuralVirtual.API.Models.Auth;
using MuralVirtual.API.Resources;

namespace MuralVirtual.API.Validators.Auth;

public class RegisterModelValidator : AbstractValidator<RegisterModel>
{
    public RegisterModelValidator()
    {
        #region FullName
        RuleFor(model => model.FullName)
            .NotEmpty()
            .WithMessage(x => string.Format(ApiMessages.Field_Required, nameof(x.FullName)));

        RuleFor(model => model.FullName!.Trim())
            .MinimumLength(3)
            .WithMessage(x => string.Format(ApiMessages.Field_GreaterThanOrEqual, nameof(x.FullName), 3))
            .When(model => !string.IsNullOrWhiteSpace(model.FullName));
        #endregion FullName

        #region Email
        RuleFor(model => model.Email)
            .NotEmpty()
            .WithMessage(x => string.Format(ApiMessages.Field_Required, nameof(x.Email)));

        RuleFor(model => model.Email)
            .EmailAddress()
            .WithMessage(x => string.Format(ApiMessages.Field_Invalid, nameof(x.Email)));
        #endregion Email

        #region Password
        RuleFor(model => model.Password)
            .NotEmpty()
            .WithMessage(x => string.Format(ApiMessages.Field_Required, nameof(x.Password)));

        RuleFor(model => model.Password)
            .MinimumLength(6)
            .WithMessage(x => string.Format(ApiMessages.Field_GreaterThanOrEqual, nameof(x.Password), 6));

        RuleFor(model => model.Password)
            .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).+$")
            .WithMessage(string.Format(ApiMessages.Auth_Password_Required_Characters));
        #endregion Password

        #region ConfirmationPassword
        RuleFor(model => model.ConfirmationPassword)
           .NotEmpty()
           .WithMessage(x => string.Format(ApiMessages.Field_Required, nameof(x.ConfirmationPassword)));

        RuleFor(model => model.ConfirmationPassword)
           .Must((model, confirmationPassowrd) => model.Password == confirmationPassowrd)
           .WithMessage(ApiMessages.Auth_ConfirmationPassword_NotEquivalent);
        #endregion ConfirmationPassword
            
    }
}