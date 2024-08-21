using Microsoft.EntityFrameworkCore;
using Library.DataAccess.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
	public class AuthorConfiguration: IEntityTypeConfiguration<AuthorEntity>
	{
		public void Configure(EntityTypeBuilder<AuthorEntity> builder)
		{
			builder.HasKey(a => a.Id);

            builder.ToTable("author");

            builder
                .Property(author => author.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(author => author.Surname)
                .IsRequired()
                .HasMaxLength(100);



            builder
                .HasMany(a => a.Books)
				.WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);



        }
	}
}

