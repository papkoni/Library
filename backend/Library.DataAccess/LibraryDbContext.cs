using Library.DataAccess.Configurations;
using Library.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess
{
	public class LibraryDbContext: DbContext
	{
        public virtual DbSet<AuthorEntity> Authors { get; set; }
        public virtual DbSet<BookEntity> Books { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

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

