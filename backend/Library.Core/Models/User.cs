using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;

namespace Library.Core.Models
{
	public class User
	{
        public Guid Id { get; set; }

        public string Role { get; set; }


        public string Name { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public Guid RefreshTokenId { get; set; }

        public RefreshToken? RefreshToken { get; set; }

        public List<Book>? Books { get; set; }

        
        private User() { }


        public User(Guid id, string name, string passwordHash, string email, string role)
		{
            Id = id;
            Name = name;
            PasswordHash = passwordHash;
            Email = email;
            Role = role;
        }

        

        public static User Create(Guid id, string name, string passwordHash, string email, string role)
        {
            return new User(id, name, passwordHash, email, role);
        }
	}
}

