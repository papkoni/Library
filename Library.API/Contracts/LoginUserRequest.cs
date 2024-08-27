using System;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Contracts
{
	public record LoginUserRequest (
		[Required] string Email,
        [Required] string Password 
        );
	
}

