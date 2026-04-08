using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinCoach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLookupsWithDataAnnotations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PhoneCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Flag", "IsEnabled", "Name", "PhoneCode" },
                values: new object[,]
                {
                    { new Guid("16fbaeb5-4eb9-4645-8199-343453a6bb65"), "US", null, true, "United States", "+1" },
                    { new Guid("17453330-7ad0-4bd1-9aae-8c986468f92a"), "IN", null, true, "India", "+91" },
                    { new Guid("a2cfa51a-64e0-40b0-adcb-3bb6d8543719"), "AE", null, true, "United Arab Emirates", "+971" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "IsEnabled", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("112ee0a8-ea7c-4b0e-893f-9aba8d7551cc"), "INR", true, "Indian Rupee", "₹" },
                    { new Guid("3c00134f-edbe-496e-8eef-1445f739785c"), "AED", true, "UAE Dirham", "د.إ" },
                    { new Guid("55508f95-d2ae-45dc-a34a-3c0b74fa9258"), "USD", true, "US Dollar", "$" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Code",
                table: "Countries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
