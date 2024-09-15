using Library.Core.Models;
using Library.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess
{
	public class LibraryDbContext: DbContext
	{
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public LibraryDbContext()
		{
		}
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());


            base.OnModelCreating(modelBuilder);
        }




    }
}

