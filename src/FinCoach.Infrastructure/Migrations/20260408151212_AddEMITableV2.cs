using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinCoach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEMITableV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("112ee0a8-ea7c-4b0e-893f-9aba8d7551cc"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("3c00134f-edbe-496e-8eef-1445f739785c"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("55508f95-d2ae-45dc-a34a-3c0b74fa9258"));

            migrationBuilder.CreateTable(
                name: "EMIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OriginalPrincipal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalTenure = table.Column<int>(type: "int", nullable: false),
                    PaidEmis = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMIs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EMIs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("16fbaeb5-4eb9-4645-8199-343453a6bb65"),
                column: "Flag",
                value: "🇺🇸");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("17453330-7ad0-4bd1-9aae-8c986468f92a"),
                column: "Flag",
                value: "🇮🇳");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a2cfa51a-64e0-40b0-adcb-3bb6d8543719"),
                column: "Flag",
                value: "🇦🇪");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "IsEnabled", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("138e2948-5394-4e20-8885-a6377d67b45a"), "INR", true, "Indian Rupee", "₹" },
                    { new Guid("245c8ecd-5403-4ebc-966a-a5ea949a1ff7"), "AED", true, "UAE Dirham", "د.إ" },
                    { new Guid("72e54e3c-4a11-4f0a-8223-69d310220d26"), "USD", true, "US Dollar", "$" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMIs_UserId",
                table: "EMIs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMIs");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("138e2948-5394-4e20-8885-a6377d67b45a"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("245c8ecd-5403-4ebc-966a-a5ea949a1ff7"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("72e54e3c-4a11-4f0a-8223-69d310220d26"));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("16fbaeb5-4eb9-4645-8199-343453a6bb65"),
                column: "Flag",
                value: null);

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("17453330-7ad0-4bd1-9aae-8c986468f92a"),
                column: "Flag",
                value: null);

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a2cfa51a-64e0-40b0-adcb-3bb6d8543719"),
                column: "Flag",
                value: null);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "IsEnabled", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("112ee0a8-ea7c-4b0e-893f-9aba8d7551cc"), "INR", true, "Indian Rupee", "₹" },
                    { new Guid("3c00134f-edbe-496e-8eef-1445f739785c"), "AED", true, "UAE Dirham", "د.إ" },
                    { new Guid("55508f95-d2ae-45dc-a34a-3c0b74fa9258"), "USD", true, "US Dollar", "$" }
                });
        }
    }
}
