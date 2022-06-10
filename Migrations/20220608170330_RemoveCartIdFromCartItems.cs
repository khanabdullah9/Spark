using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spark.Migrations
{
    public partial class RemoveCartIdFromCartItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_carts_CartId",
                table: "cartItems");

            migrationBuilder.AlterColumn<string>(
                name: "CartId",
                table: "cartItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_carts_CartId",
                table: "cartItems",
                column: "CartId",
                principalTable: "carts",
                principalColumn: "CartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_carts_CartId",
                table: "cartItems");

            migrationBuilder.AlterColumn<string>(
                name: "CartId",
                table: "cartItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_carts_CartId",
                table: "cartItems",
                column: "CartId",
                principalTable: "carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
