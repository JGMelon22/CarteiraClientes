using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarteiraClientes.Migrations
{
    /// <inheritdoc />
    public partial class DocumentAndCompanyNameFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "companies",
                newName: "company_name");

            migrationBuilder.RenameColumn(
                name: "Document",
                table: "clients",
                newName: "document");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "company_name",
                table: "companies",
                newName: "CompanyName");

            migrationBuilder.RenameColumn(
                name: "document",
                table: "clients",
                newName: "Document");
        }
    }
}
