using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entites
{

    public class BookEntity
	{
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? RecieveDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string Genre { get; set; } = string.Empty;

        public Guid AuthorId { get; set; }

        public required AuthorEntity Author { get; set; }

        public string ImageName { get; set; } = string.Empty;

        public Guid? UserId { get; set; }

        public UserEntity? User { get; set; }
    }

}

