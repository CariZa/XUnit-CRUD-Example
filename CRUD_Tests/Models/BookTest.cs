using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRUD_Razor_2_1.Models;
using Xunit;

namespace CRUD_Tests.Models
{
    public class BookTest
    {
         
        [Fact]
        public void BookModel_Instantiates()
        {
            string book = "Harry Potter";
            string author = "JK Rowling";
            string isbn = "123234345";

            Book bookInst = new Book() {
                Name = book,
                Author = author,
                ISBN = isbn
            };

            Assert.Matches(bookInst.Name, book);
            Assert.Matches(bookInst.Author, author);
            Assert.Matches(bookInst.ISBN, isbn);

            // Check no validation errors
            Assert.False(ValidateModel(bookInst).Count > 0);
        }

        [Fact]
        public void BookModel_RequiresNameField()
        {
            string author = "JK Rowling";
            string isbn = "123234345";

            Book bookInst = new Book()
            {
                Author = author,
                ISBN = isbn
            };

            var invalidFields = ValidateModel(bookInst);

            // Validation errors should return
            Assert.True(invalidFields.Count > 0);
        }

        [Fact]
        public void BookModel_DoesNotRequireOtherFields()
        {
            string book = "Harry Potter";
            Book bookInst = new Book()
            {
                Name = book
            };

            var invalidFields = ValidateModel(bookInst);
            Assert.False(invalidFields.Count > 0);
        }

        // Validation Helper
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(ctx);
            return validationResults;
        }
    }
}
