using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_Core.Migrations
{
    public partial class Version113 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Region",
                table: "Territories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Region",
                table: "Region");

            migrationBuilder.RenameTable(
                name: "Region",
                newName: "Regions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "RegionID")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Regions",
                table: "Territories",
                column: "RegionsID",
                principalTable: "Regions",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Regions",
                table: "Territories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Region");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Region",
                table: "Region",
                column: "RegionID")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Region",
                table: "Territories",
                column: "RegionsID",
                principalTable: "Region",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
