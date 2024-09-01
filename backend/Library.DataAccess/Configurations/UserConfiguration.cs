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
               .IsRequired()
               .HasMaxLength(100);

            builder
               .Property(u => u.PasswordHash)
               .IsRequired();

            builder
               .Property(u => u.RefreshTokenId)
               .IsRequired();

            builder.HasOne(u => u.RefreshToken) 
              .WithOne(r => r.User)        
              .HasForeignKey<UserEntity>(u => u.RefreshTokenId);
        }

    }
}



