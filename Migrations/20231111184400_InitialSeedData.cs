using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarteiraClientes.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "client_id", "age", "document", "full_name", "gender", "is_overdue" },
                values: new object[,]
                {
                    { 1, (byte)18, "123456789", "Allissa Victória da Silva", "Female", false },
                    { 2, (byte)25, "987654321", "João Oliveira", "Male", true },
                    { 3, (byte)30, "456789123", "Maria Santos", "Female", false },
                    { 4, (byte)22, "234567890", "Carlos Pereira", "Male", true },
                    { 5, (byte)28, "345678901", "Ana Oliveira", "Female", false },
                    { 6, (byte)35, "456789012", "Pedro Silva", "Male", true },
                    { 7, (byte)40, "567890123", "Camila Santos", "Female", false },
                    { 8, (byte)32, "678901234", "Felipe Oliveira", "Male", true },
                    { 9, (byte)27, "789012345", "Juliana Silva", "Female", false },
                    { 10, (byte)24, "890123456", "Lucas Pereira", "Male", true }
                });

            migrationBuilder.InsertData(
                table: "companies",
                columns: new[] { "company_id", "company_name", "founded_date", "revenue" },
                values: new object[,]
                {
                    { 1, "Banco do Brasil", new DateTime(1922, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28820000000.00m },
                    { 2, "Tech Solutions Inc.", new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15500000000.00m },
                    { 3, "Global Motors", new DateTime(1985, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10200000000.00m },
                    { 4, "Food Delights Co.", new DateTime(2010, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5750000000.00m },
                    { 5, "Fashion Trends Ltd.", new DateTime(1995, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 8300000000.00m },
                    { 6, "Green Energy Solutions", new DateTime(2008, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3600000000.00m }
                });

            migrationBuilder.InsertData(
                table: "clients_companies",
                columns: new[] { "client_id", "company_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 3 },
                    { 4, 4 },
                    { 4, 5 },
                    { 4, 6 },
                    { 5, 1 },
                    { 5, 2 },
                    { 5, 3 },
                    { 5, 4 },
                    { 5, 5 },
                    { 5, 6 },
                    { 6, 1 },
                    { 6, 2 },
                    { 6, 3 },
                    { 6, 4 },
                    { 6, 5 },
                    { 6, 6 },
                    { 7, 1 },
                    { 7, 2 },
                    { 7, 3 },
                    { 7, 4 },
                    { 7, 5 },
                    { 7, 6 },
                    { 8, 1 },
                    { 8, 2 },
                    { 8, 3 },
                    { 8, 5 },
                    { 8, 6 },
                    { 9, 1 },
                    { 9, 5 },
                    { 10, 1 },
                    { 10, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 9, 5 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "clients_companies",
                keyColumns: new[] { "client_id", "company_id" },
                keyValues: new object[] { 10, 5 });

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "client_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "client_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "client_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "client_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "client_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "client_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "client_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "client_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "client_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "clients",
                keyColumn: "client_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "company_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "company_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "company_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "company_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "company_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "company_id",
                keyValue: 6);
        }
    }
}
