using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mohkazv.Library.WebApp.Areas.Identity.Data;
using Mohkazv.Library.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mohkazv.Library.WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Models.Type> Types { get; set; }

        public DbSet<DeweyDecimalClassification> DeweyDecimalClassifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map many Book(s) with many Author(s) in BookAuthor(s)
            modelBuilder.Entity<BookAuthor>().ToTable("BookAuthors");
            modelBuilder.Entity<BookAuthor>()
                .HasKey(bookAuthor => new { bookAuthor.BookId, bookAuthor.AuthorId });
            modelBuilder.Entity<BookAuthor>()
                .HasOne(bookAuthor => bookAuthor.Book)
                .WithMany(book => book.BookAuthors)
                .HasForeignKey(BookAuthor => BookAuthor.BookId);
            modelBuilder.Entity<BookAuthor>()
                .HasOne(bookAuthor => bookAuthor.Author)
                .WithMany(author => author.BookAuthors)
                .HasForeignKey(bookAuthor => bookAuthor.AuthorId);

            // Map many Book(s) with many User(s) in BorrowBook(s)
            modelBuilder.Entity<BorrowBook>().ToTable("BorrowBooks");
            modelBuilder.Entity<BorrowBook>()
                .HasKey(borrowBook => new { borrowBook.BookId, borrowBook.UserId });
            modelBuilder.Entity<BorrowBook>()
                .HasOne(borrowBook => borrowBook.Book)
                .WithMany(book => book.BorrowBooks)
                .HasForeignKey(borrowBook => borrowBook.BookId);
            modelBuilder.Entity<BorrowBook>()
                .HasOne(borrowBook => borrowBook.User)
                .WithMany(user => user.BorrowBooks)
                .HasForeignKey(borrowBook => borrowBook.UserId);
        }

        public DbSet<Mohkazv.Library.WebApp.Models.BookAuthor> BookAuthor { get; set; }

        public DbSet<Mohkazv.Library.WebApp.Models.BorrowBook> BorrowBook { get; set; }
    }
}
