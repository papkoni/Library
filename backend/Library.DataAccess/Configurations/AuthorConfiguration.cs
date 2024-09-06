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
                .Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(a => a.Surname)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(a => a.Birthday)
                .IsRequired()
                .HasConversion(
                    v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null, // Преобразование к UTC при сохранении, если значение не null
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null);

            builder
                .Property(a => a.Country)
                .IsRequired(false)
                .HasMaxLength(50);

            builder
                .HasMany(a => a.Books)
				.WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);



        }
	}
}

