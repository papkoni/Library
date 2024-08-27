using System;
namespace Library.API.Contracts
{
	public record RegisterUserResponse(
		Guid id,
		string name,
		string email
		);
	
}

