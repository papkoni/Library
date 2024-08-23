using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;

namespace Library.Core.Models
{
	public class User
	{
        public Guid Id { get;  }

        public string Login { get;  } = string.Empty;

        public string Password { get;  } = string.Empty;



        private User(Guid id, string login, string password)
		{
            Id = id;
            Login = login;
            Password = password;
        }

        public static Result<User> Create(Guid id, string login, string password)
        {
            if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return Result.Failure<User>($"login or password can't be null or empty");
            }

            var user = new User(id, login, password);

            return Result.Success(user);
        }
	}
}

