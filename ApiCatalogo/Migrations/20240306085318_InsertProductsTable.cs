using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class InsertProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Products(Name, Description, Price, UrlImage, Stock, RegisterDate, CategoryId)" +
                   "VALUES ('Laptop', 'Powerful laptop for work and gaming', 1299.99, 'laptop.jpg', 50, NOW(), 2)");

            mb.Sql("INSERT INTO Products(Name, Description, Price, UrlImage, Stock, RegisterDate, CategoryId)" +
                   "VALUES ('Smartphone', 'Latest smartphone with advanced features', 799.99, 'smartphone.jpg', 100, NOW(), 2)");

            mb.Sql("INSERT INTO Products(Name, Description, Price, UrlImage, Stock, RegisterDate, CategoryId)" +
                   "VALUES ('Running Shoes', 'Comfortable running shoes for all terrains', 89.99, 'runningshoes.jpg', 200, NOW(), 3)");

            mb.Sql("INSERT INTO Products(Name, Description, Price, UrlImage, Stock, RegisterDate, CategoryId)" +
                   "VALUES ('Cookware Set', 'High-quality cookware set for your kitchen', 249.99, 'cookwareset.jpg', 30, NOW(), 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }
    }
}
