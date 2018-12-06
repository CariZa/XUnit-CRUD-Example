using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Razor_2_1.Models;
using CRUD_Razor_2_1.Pages.BookList;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CRUD_Tests.Pages.BookList
{
    public class EditTest
    {
        [Fact]
        public async void Edit_OnGet_SetBookUsingId()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDb_Edit");
            builder.EnableSensitiveDataLogging(true);
            var mockDbContext = new ApplicationDbContext(builder.Options);

            Seed(mockDbContext);

            // Act
            var editPage = new EditModel(mockDbContext);
            editPage.OnGet(2);

            // Asssert
            var assignedBook = Assert.IsAssignableFrom<Book>(editPage.Book);
            Assert.Matches("moo2", assignedBook.ISBN);
            Assert.Matches("Author2", assignedBook.Author);
            Assert.Matches("Name2", assignedBook.Name);

            Assert.Null(editPage.Message);

            // Teardown
            await Teardown(mockDbContext);
        }

        [Fact]
        public async void Edit_OnGet_EditBookEntryIfValid()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDb_Edit");
            var context = new ApplicationDbContext(builder.Options);
            Seed(context);

            // Act
            var editPage = new EditModel(context);
            editPage.OnGet(2);

            editPage.Book.Author = "Test2";
            editPage.Book.ISBN = "Test2";
            editPage.Book.Name = "Test2";

            await editPage.OnPost();

            var books = await context.Books.ToListAsync();

            // Assert
            Assert.Equal(editPage.Book, books[1]);
            Assert.Matches(books[1].Name, "Test2");
            Assert.Matches(books[1].ISBN, "Test2");
            Assert.Matches(books[1].Author, "Test2");

            Assert.Matches(editPage.Message, "Book has been updated successfully");

            await Teardown(context);
        }

        [Fact]
        public async void Edit_OnGet_DoNotEditBookEntryIfInvalid()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDb_Edit");
            var context = new ApplicationDbContext(builder.Options);
            Seed(context);

            // Act
            var editPage = new EditModel(context);
            editPage.OnGet(2);

            editPage.Book = new Book()
            {
                Id = 2,
                Author = "moo",
                ISBN = "baa",
                Name = null
            };

            editPage.ModelState.AddModelError("", "err");

            await editPage.OnPost();


            var books = await context.Books.ToListAsync();

            Assert.Matches(books[1].Name, "Name2");
            Assert.NotNull(books[1].Name);

            await Teardown(context);
        }

        // Seed

        private void Seed(ApplicationDbContext context)
        {
            var books = new[]
            {
                new Book() { Name = "Name1", Author = "Author1", ISBN = "moo1", Id = 1},
                new Book() { Name = "Name2", Author = "Author2", ISBN = "moo2", Id = 2},
                new Book() { Name = "Name3", Author = "Author3", ISBN = "moo3", Id = 3}
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

