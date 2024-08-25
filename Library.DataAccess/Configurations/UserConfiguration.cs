using Library.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
		
		public void Configure(EntityTypeBuilder<UserEntity> builder)
		{
            builder.HasKey(u => u.Id);

            builder.ToTable("user");

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .Property(u => u.Email)
                .IsRequired();

            builder
               .Property(u => u.Name)
               .IsRequired();

            builder
               .Property(u => u.PasswordHash)
               .IsRequired();
        }

    }
}



