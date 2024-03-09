using FluentValidation;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Application.Models.DtoValidators.AccountDtoValidators;

public class AuthenticatedAccountDtoValidator : AbstractValidator<AuthenticatedAccountDto>
{
    public AuthenticatedAccountDtoValidator()
    {
        RuleFor(dto => dto.Account)
            .NotEmpty()
            .WithMessage("Account doesn't exists yet.");
        RuleFor(dto => dto.Token)
            .NotEmpty()
            .MinimumLength(8) // Не знаю какая будет длина у токена
            .WithMessage("Token is too short.")
            .Matches("[^A-Za-zА-ЯЁа-яё0-9 ,.!@#$;%:&?*()_+=-]"); // Тоже надо подумать
        RuleFor(dto => dto.IsAdmin)
            .NotNull();
    }
}