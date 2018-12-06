using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_Razor_2_1.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Books (Name, ISBN, Author) VALUES ('Harry Potter', '1232345345', 'JK Rowling')");
            migrationBuilder.Sql("INSERT INTO Books (Name, ISBN, Author) VALUES ('Fight Club', '432523452', 'Chuck Palanhiuk')");
            migrationBuilder.Sql("INSERT INTO Books (Name, ISBN, Author) VALUES ('Lord of the rings', '8347236267', 'JRR Tolkien')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Books WHERE ISBN IN ('1232345345','432523452','8347236267')");
        }
    }
}
