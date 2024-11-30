using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZayShop.Migrations
{
    /// <inheritdoc />
    public partial class BrandAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoPath1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoPath2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoPath3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoPath4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
