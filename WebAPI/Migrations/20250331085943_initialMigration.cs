using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    position = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cote = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carres_Formes_Id",
                        column: x => x.Id,
                        principalTable: "Formes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cercles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Rayon = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cercles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cercles_Formes_Id",
                        column: x => x.Id,
                        principalTable: "Formes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Placements",
                columns: table => new
                {
                    Position = table.Column<string>(type: "text", nullable: false),
                    FormeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placements", x => x.Position);
                    table.ForeignKey(
                        name: "FK_Placements_Formes_FormeId",
                        column: x => x.FormeId,
                        principalTable: "Formes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Rectangles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Longueur = table.Column<double>(type: "double precision", nullable: false),
                    Largeur = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rectangles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rectangles_Formes_Id",
                        column: x => x.Id,
                        principalTable: "Formes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Triangles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cote = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triangles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triangles_Formes_Id",
                        column: x => x.Id,
                        principalTable: "Formes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Placements_FormeId",
                table: "Placements",
                column: "FormeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carres");

            migrationBuilder.DropTable(
                name: "Cercles");

            migrationBuilder.DropTable(
                name: "Placements");

            migrationBuilder.DropTable(
                name: "Rectangles");

            migrationBuilder.DropTable(
                name: "Triangles");

            migrationBuilder.DropTable(
                name: "Formes");
        }
    }
}
