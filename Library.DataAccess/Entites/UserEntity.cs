﻿using System;
namespace Library.DataAccess.Entites
{
	public class UserEntity
	{
        public Guid Id { get; set; }

        public string Name { get;  set; } = string.Empty;

        public string PasswordHash { get;  set; } = string.Empty;

        public string Email { get;  set; } = string.Empty;

    }
}

