using FluentValidation;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Application.Models.DtoValidators.AccountDtoValidators;

public class RegisterAccountDtoValidator : AbstractValidator<RegisterAccountDto>
{
    public RegisterAccountDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("This name is too long.")
            .Must(c => c.All(char.IsLetter));
        RuleFor(dto => dto.Surname)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("This surname is too long.")
            .Must(c => c.All(char.IsLetter));
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email address is invalid.");
        RuleFor(dto => dto.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("This password is too short.");
    }
}