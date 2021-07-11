using Microsoft.EntityFrameworkCore.Migrations;

namespace PartsPractice.Migrations
{
    public partial class WholeSalePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "WholeSalePrice",
                table: "PartsItems",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WholeSalePrice",
                table: "PartsItems");
        }
    }
}
