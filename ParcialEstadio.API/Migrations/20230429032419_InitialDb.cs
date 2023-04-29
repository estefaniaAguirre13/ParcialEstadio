using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcialEstadio.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Porterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porterias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boletas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaUso = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    Usada = table.Column<bool>(type: "bit", maxLength: 100, nullable: false),
                    PorteriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boletas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boletas_Porterias_PorteriaId",
                        column: x => x.PorteriaId,
                        principalTable: "Porterias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boletas_PorteriaId",
                table: "Boletas",
                column: "PorteriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boletas");

            migrationBuilder.DropTable(
                name: "Porterias");
        }
    }
}
