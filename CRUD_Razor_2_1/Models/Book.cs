using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Razor_2_1.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //[Required]
        public string ISBN { get; set; }

        public string Author { get; set; }
    }
}
