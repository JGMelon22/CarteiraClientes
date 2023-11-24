using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarteiraClientes.Migrations
{
    /// <inheritdoc />
    public partial class SqlServerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    client_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    age = table.Column<short>(type: "smallint", nullable: false),
                    document = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false),
                    gender = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    is_overdue = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    founded_date = table.Column<DateTime>(type: "date", nullable: false),
                    revenue = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.company_id);
                });

            migrationBuilder.CreateTable(
                name: "clients_companies",
                columns: table => new
                {
                    client_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients_companies", x => new { x.client_id, x.company_id });
                    table.ForeignKey(
                        name: "FK_clients_companies_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "client_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_clients_companies_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "client_id_idx",
                table: "clients",
                column: "client_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "client_company_client_id_idx",
                table: "clients_companies",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "client_company_company_id_idx",
                table: "clients_companies",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "company_id_idx",
                table: "companies",
                column: "company_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clients_companies");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "companies");
        }
    }
}
