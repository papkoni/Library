using System;
namespace Library.Core.Models
{
	public class User
	{
        public Guid Id { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;



        public User()
		{
		}
	}
}

