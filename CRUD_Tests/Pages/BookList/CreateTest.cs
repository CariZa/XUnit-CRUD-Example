using System;
using CRUD_Razor_2_1.Models;
using CRUD_Razor_2_1.Pages.BookList;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CRUD_Tests.Pages.BookList
{
    public class CreateTest
    {
        [Fact]
        public async System.Threading.Tasks.Task Create_OnPost_BookShouldBeAddedAsync()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDb_Create");
            var context = new ApplicationDbContext(builder.Options);
            Seed(context);

            // Act
            var model = new CreateModel(context);

            var book = new Book()
            {
                Name = "NameTest",
                ISBN = "ISBNTsst",
                Author = "AuthorTest"
            };

            await model.OnPost(book);

            // Assert
            var books = await context.Books.ToListAsync();
            Assert.Equal(4, books.Count);
            Assert.Matches(books[3].Name, "NameTest");

        }

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
    }
}