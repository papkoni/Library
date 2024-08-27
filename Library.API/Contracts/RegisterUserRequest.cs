using System.ComponentModel.DataAnnotations;

namespace Library.API.Contracts
{
	public record RegisterUserRequest
	(
        [Required]string Email,
        [Required] string Password,
        [Required] string Name

    );
}

