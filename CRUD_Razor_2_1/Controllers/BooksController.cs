using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Razor_2_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD_Razor_2_1.Controllers
{
    [Route("v1/books")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Book> books;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: api/values
        [HttpGet]
        public async Task<string> GetAsync()
        {
            //return new string[] { "value1", "value2" };
            books = await _db.Books.ToListAsync();

            //using Newtonsoft.Json;
            // ...
            var output = JsonConvert.SerializeObject(books);

            //return books.ToString();
            return output;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
