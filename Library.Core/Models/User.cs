using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;

namespace Library.Core.Models
{
	public class User
	{
        public Guid Id { get; set; }

        public string Name { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        private User(Guid id, string name, string passwordHash, string email)
		{
            Id = id;
            Name = name;
            PasswordHash = passwordHash;
            Email = email;
        }

        public static User Create(Guid id, string name, string passwordHash, string email)
        {
            return new User(id, name, passwordHash, email);
        }
	}
}

