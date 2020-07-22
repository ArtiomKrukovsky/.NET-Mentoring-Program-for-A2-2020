using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_Core.Migrations
{
    public partial class Version11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    CreditCardID = table.Column<int>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    CardNumber = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    CardHolder = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => new { x.CreditCardID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_Employee_Credit_Cards",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "CardHolder",
                table: "CreditCard",
                column: "CardHolder");

            migrationBuilder.CreateIndex(
                name: "CardNumber",
                table: "CreditCard",
                column: "CardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_EmployeeID",
                table: "CreditCard",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "ExpirationDate",
                table: "CreditCard",
                column: "ExpirationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCard");
        }
    }
}
