using System;
using Library.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.Id);

            builder.ToTable("refreshToken");

            builder.Property(r => r.ExpiryDate)
                  .IsRequired()
                  .HasConversion(
                v => v.ToUniversalTime(), // Преобразование к UTC перед сохранением
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Преобразование к UTC при загрузке;

            builder.Property(r => r.Token)
                   .IsRequired();

            builder.HasOne(r => r.User)
                   .WithOne(u => u.RefreshToken)
                   .HasForeignKey<User>(u => u.RefreshTokenId);

        }
    }

}

