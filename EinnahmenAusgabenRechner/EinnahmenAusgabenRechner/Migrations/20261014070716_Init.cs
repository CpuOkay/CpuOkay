using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EinnahmenAusgabenRechner.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EintragTyp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EintragTyp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Farbe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eintrag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Betrag = table.Column<double>(type: "float", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Beschreibung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KategorieId = table.Column<int>(type: "int", nullable: false),
                    EintragTypId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eintrag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eintrag_EintragTyp_EintragTypId",
                        column: x => x.EintragTypId,
                        principalTable: "EintragTyp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eintrag_Kategorie_KategorieId",
                        column: x => x.KategorieId,
                        principalTable: "Kategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eintrag_EintragTypId",
                table: "Eintrag",
                column: "EintragTypId");

            migrationBuilder.CreateIndex(
                name: "IX_Eintrag_KategorieId",
                table: "Eintrag",
                column: "KategorieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eintrag");

            migrationBuilder.DropTable(
                name: "EintragTyp");

            migrationBuilder.DropTable(
                name: "Kategorie");
        }
    }
}
