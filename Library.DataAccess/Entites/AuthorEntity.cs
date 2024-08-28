using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entites
{
    public class AuthorEntity 
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public DateTime Birthday { get; set; }

        public string? Country { get; set; } = string.Empty;

        public List<BookEntity>? Books { get; set; }

    }
}

