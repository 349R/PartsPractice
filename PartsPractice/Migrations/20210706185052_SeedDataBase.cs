using Microsoft.EntityFrameworkCore.Migrations;

namespace PartsPractice.Migrations
{
    public partial class SeedDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PartsItems",
                columns: new[] { "PartId", "Available", "Description", "Name", "Price", "QtyOnHand" },
                values: new object[] { 1L, true, "Ball pein Hammer", "Hammer", 44.44m, 100 });

            migrationBuilder.InsertData(
                table: "PartsItems",
                columns: new[] { "PartId", "Available", "Description", "Name", "Price", "QtyOnHand" },
                values: new object[] { 2L, true, "Flat head", "ScrewDriver", 10.44m, 50 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PartsItems",
                keyColumn: "PartId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "PartsItems",
                keyColumn: "PartId",
                keyValue: 2L);
        }
    }
}
