using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_Core.Migrations
{
    public partial class AlterDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName", "Description", "Picture" },
                values: new object[,]
                {
                    { 4, "Phones", "Tech", null },
                    { 5, "Cars", "Tesla", null },
                    { 6, "Monitors", "Full Hd", null },
                    { 7, "CPU", "Intel i9", null }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "RegionID", "RegionDescription" },
                values: new object[,]
                {
                    { 1, "This is a beautiful region" },
                    { 2, "This is a not good region for doing business" }
                });

            migrationBuilder.InsertData(
                table: "Territories",
                columns: new[] { "TerritoryID", "RegionsID", "TerritoryDescription" },
                values: new object[] { "Gdansk", 1, "This is a beautiful territory" });

            migrationBuilder.InsertData(
                table: "Territories",
                columns: new[] { "TerritoryID", "RegionsID", "TerritoryDescription" },
                values: new object[] { "Grodno", 2, "This is a beautiful territory" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "TerritoryID",
                keyValue: "Gdansk");

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "TerritoryID",
                keyValue: "Grodno");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 2);
        }
    }
}
