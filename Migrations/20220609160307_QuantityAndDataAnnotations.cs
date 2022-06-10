using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spark.Migrations
{
    public partial class QuantityAndDataAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "cartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "cartItems");
        }
    }
}
