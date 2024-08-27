using System;
using Library.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.HasKey(r => r.Id);

            builder.ToTable("refreshToken");

            builder.Property(r => r.ExpiryDate)
                  .IsRequired();

            builder.Property(r => r.Token)
                   .IsRequired();

            builder.HasOne(r => r.User)
                   .WithOne(u => u.RefreshToken)
                   .HasForeignKey<UserEntity>(u => u.RefreshTokenId);

        }
    }

}

