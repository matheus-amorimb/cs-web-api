using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class FillCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categories(Name, UrlImage) VALUES ('Food', 'food.jpg')");
            mb.Sql("INSERT INTO Categories(Name, UrlImage) VALUES ('Electronics', 'electronics.jpg')");
            mb.Sql("INSERT INTO Categories(Name, UrlImage) VALUES ('Clothing', 'clothing.jpg')");
            mb.Sql("INSERT INTO Categories(Name, UrlImage) VALUES ('Books', 'books.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Categories");
        }
    }
}
