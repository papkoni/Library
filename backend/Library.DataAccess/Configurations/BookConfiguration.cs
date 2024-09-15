using System;
using Library.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
	public class BookConfiguration: IEntityTypeConfiguration<Book>
    {

       

        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.ToTable("book");



            builder
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(300);

            builder
                .HasIndex(b => b.ISBN)
                .IsUnique();

            builder
                .Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(b => b.ReturnDate)
                .IsRequired(false)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null, // Преобразование к UTC при сохранении, если значение не null
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null);

            builder
                .Property(b => b.RecieveDate)
                .IsRequired(false)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null, // Преобразование к UTC при сохранении, если значение не null
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null);

            builder
              .Property(b => b.Genre)
              .IsRequired();

            builder
              .Property(b => b.ImageName)
              .IsRequired();


            builder
                .Property(b => b.AuthorId)
                .IsRequired();

            builder
                .Property(b => b.UserId)
                .IsRequired(false);
        }

	}
}

