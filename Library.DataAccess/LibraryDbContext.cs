using Library.DataAccess.Configurations;
using Library.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess
{
	public class LibraryDbContext: DbContext
	{
        public virtual DbSet<AuthorEntity> Author { get; set; }
        public virtual DbSet<BookEntity> Book { get; set; }


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


            base.OnModelCreating(modelBuilder);
        }




    }
}

