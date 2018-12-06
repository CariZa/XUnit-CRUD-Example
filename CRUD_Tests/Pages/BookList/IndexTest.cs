using System;
using System.Threading.Tasks;
using CRUD_Razor_2_1.Models;
using CRUD_Razor_2_1.Pages.BookList;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.

using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace CRUD_Tests.Pages.BookList
{

    public class IndexTest
    {
        [Fact]
        public void Index_Init_BooksNull()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb_Index");
            var mockAppDbContext = new ApplicationDbContext(builder.Options);

            // Act
            var pageModel = new IndexModel(mockAppDbContext);

            // Assert
            Assert.Null(pageModel.Books);
        }

        [Fact]
        public async void Index_OnGet_BooksShouldSet()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb_Index");
            var mockAppDbContext = new ApplicationDbContext(builder.Options);

            Seed(mockAppDbContext);

            var pageModel = new IndexModel(mockAppDbContext);

            // Act
            await pageModel.OnGet();

            // Assert
            var actualMessages = Assert.IsAssignableFrom<List<Book>>(pageModel.Books);
            Assert.Equal(3, actualMessages.Count);

            await Teardown(mockAppDbContext);
        }

        [Fact]
        public async void Index_OnPostDelete_BookGetsDeleted()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb_Index");
            var mockAppDbContext = new ApplicationDbContext(builder.Options);

            Seed(mockAppDbContext);

            var pageModel = new IndexModel(mockAppDbContext);

            // Act
            var deleteBooks = await mockAppDbContext.Books.ToListAsync();
            await pageModel.OnPostDelete(deleteBooks[1].Id);


            var books = await mockAppDbContext.Books.ToListAsync();

            // Assert
            Assert.Equal(2, books.Count);

            Assert.Matches(pageModel.Message, "Book deleted");

            await Teardown(mockAppDbContext);
        }

        // Seed

        private void Seed(ApplicationDbContext context)
        {
            var books = new[]
            {
                new Book() { Name = "Name", Author = "Author", ISBN = "moo" },
                new Book() { Name = "Name", Author = "Author", ISBN = "moo" },
                new Book() { Name = "Name", Author = "Author", ISBN = "moo" }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }

        private async Task Teardown(ApplicationDbContext context)
        {
            var books = await context.Books.ToListAsync();
            context.Books.RemoveRange(books);
            context.SaveChanges();
        }
    }
}