﻿// <auto-generated />
using System;
using Library.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library.DataAccess.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    partial class LibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Library.DataAccess.Entites.AuthorEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("author", (string)null);
                });

            modelBuilder.Entity("Library.DataAccess.Entites.BookEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("RecieveDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ISBN")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("book", (string)null);
                });

            modelBuilder.Entity("Library.DataAccess.Entites.RefreshTokenEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("refreshToken", (string)null);
                });

            modelBuilder.Entity("Library.DataAccess.Entites.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RefreshTokenId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RefreshTokenId")
                        .IsUnique();

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Library.DataAccess.Entites.BookEntity", b =>
                {
                    b.HasOne("Library.DataAccess.Entites.AuthorEntity", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.DataAccess.Entites.UserEntity", "User")
                        .WithMany("Books")
                        .HasForeignKey("UserId");

                    b.Navigation("Author");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Library.DataAccess.Entites.UserEntity", b =>
                {
                    b.HasOne("Library.DataAccess.Entites.RefreshTokenEntity", "RefreshToken")
                        .WithOne("User")
                        .HasForeignKey("Library.DataAccess.Entites.UserEntity", "RefreshTokenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RefreshToken");
                });

            modelBuilder.Entity("Library.DataAccess.Entites.AuthorEntity", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Library.DataAccess.Entites.RefreshTokenEntity", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Library.DataAccess.Entites.UserEntity", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
