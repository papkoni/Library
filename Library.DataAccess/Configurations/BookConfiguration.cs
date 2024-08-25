using System;
using Library.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataAccess.Configurations
{
	public class BookConfiguration: IEntityTypeConfiguration<BookEntity>
    {

       

        public void Configure(EntityTypeBuilder<BookEntity> builder)
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
                .IsRequired(false);

            builder
                .Property(b => b.RecieveDate)
                .IsRequired(false);


            builder
                .Property(b => b.AuthorId);

            
        }

	}
}

