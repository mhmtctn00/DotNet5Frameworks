using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNet5Framework.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 7, 24, 18, 44, 56, 947, DateTimeKind.Local).AddTicks(3481)),
                    Status = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Initial Category 1" },
                    { 2, "Initial Category 2" },
                    { 3, "Initial Category 3" },
                    { 4, "Initial Category 4" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Amount", "CategoryId", "CreatedDate", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 100, 1, new DateTime(2021, 7, 24, 18, 44, 56, 952, DateTimeKind.Local).AddTicks(2998), "Initial Product 1", 10m },
                    { 2, 100, 2, new DateTime(2021, 7, 24, 18, 44, 56, 952, DateTimeKind.Local).AddTicks(3500), "Initial Product 2", 10m },
                    { 3, 100, 3, new DateTime(2021, 7, 24, 18, 44, 56, 952, DateTimeKind.Local).AddTicks(3504), "Initial Product 3", 10m },
                    { 4, 100, 4, new DateTime(2021, 7, 24, 18, 44, 56, 952, DateTimeKind.Local).AddTicks(3507), "Initial Product 4", 10m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
