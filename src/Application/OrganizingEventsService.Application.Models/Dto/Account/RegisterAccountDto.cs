using System.ComponentModel.DataAnnotations;

namespace OrganizingEventsService.Application.Models.Dto.Account;

public class RegisterAccountDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required, RegularExpression("^[a-zA-Zа-яА-ЯёЁ-]{2,}$")]
    public string Surname { get; set; } = null!;
    [Required(ErrorMessage = "Email is missing"), RegularExpression("^[a-zA-Z0-9._-]+@(?:[a-zA-Z0-9]{2,}\\.)+[a-zA-Z]{2,}$", ErrorMessage = "Wrong email pattern")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Password is required"), MinLength(8), MaxLength(255)]
    public string Password { get; set; } = null!;
}