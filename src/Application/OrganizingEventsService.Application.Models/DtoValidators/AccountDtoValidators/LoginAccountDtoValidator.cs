using FluentValidation;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Application.Models.DtoValidators.AccountDtoValidators;

public class LoginAccountDtoValidator : AbstractValidator<LoginAccountDto>
{
    public LoginAccountDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is invalid");
        RuleFor(dto => dto.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("This password is too short.");
    }
}