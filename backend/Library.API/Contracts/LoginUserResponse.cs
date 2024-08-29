using System;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Contracts
{
    public record LoginUserResponse(
        [Required] Guid Id,
        [Required] string Email,
        [Required] string Name
        );
}

