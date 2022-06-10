using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spark.Migrations
{
    public partial class MultipleForeignKeysCartItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_products_ProductId",
                table: "cartItems");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "cartItems",
                newName: "productID");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_ProductId",
                table: "cartItems",
                newName: "IX_cartItems_productID");

            migrationBuilder.AlterColumn<string>(
                name: "productID",
                table: "cartItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_products_productID",
                table: "cartItems",
                column: "productID",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_products_productID",
                table: "cartItems");

            migrationBuilder.RenameColumn(
                name: "productID",
                table: "cartItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_productID",
                table: "cartItems",
                newName: "IX_cartItems_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "cartItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_products_ProductId",
                table: "cartItems",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId");
        }
    }
}
