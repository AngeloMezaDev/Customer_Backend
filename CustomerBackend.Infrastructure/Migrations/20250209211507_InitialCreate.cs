using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Company_CompanyId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customers",
                newSchema: "dbo");

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "dbo",
                columns: table => new
                {
                    company_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tax_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.company_id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_companies_company_name",
                schema: "dbo",
                table: "companies",
                column: "company_name");

            migrationBuilder.CreateIndex(
                name: "idx_companies_tax_id",
                schema: "dbo",
                table: "companies",
                column: "tax_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_companies_CompanyId",
                schema: "dbo",
                table: "Customers",
                column: "CompanyId",
                principalSchema: "dbo",
                principalTable: "companies",
                principalColumn: "company_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_companies_CompanyId",
                schema: "dbo",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "companies",
                schema: "dbo");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "dbo",
                newName: "Customers");

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Company_CompanyId",
                table: "Customers",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
