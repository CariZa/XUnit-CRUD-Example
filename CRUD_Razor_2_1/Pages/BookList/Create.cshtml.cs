using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Razor_2_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_Razor_2_1.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string Message { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        // [BindProperty] You can use this if you don't want to pass into the OnPost
        public Book Book { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(Book book)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            Message = "Book has been added";

            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");

        }
    }
}
