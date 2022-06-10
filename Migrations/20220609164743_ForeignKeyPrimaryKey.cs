using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spark.Migrations
{
    public partial class ForeignKeyPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_carts_CartId",
                table: "cartItems");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "carts",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cartItems",
                newName: "ID");

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
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_carts_CartId",
                table: "cartItems");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "carts",
                newName: "CartId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "cartItems",
                newName: "Id");

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
    }
}
