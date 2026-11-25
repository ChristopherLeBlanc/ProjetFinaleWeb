using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetFinalWeb.Migrations
{
    /// <inheritdoc />
    public partial class ajout8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acteurs",
                columns: table => new
                {
                    NoActeur = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sexe = table.Column<string>(type: "nchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acteurs", x => x.NoActeur);
                    table.CheckConstraint("CK_Acteurs_Sexe", "[Sexe] IN (N'F', N'H', N'N')");
                });

            migrationBuilder.CreateTable(
                name: "Langues",
                columns: table => new
                {
                    NoLangue = table.Column<int>(type: "int", nullable: false),
                    Langue = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Langues", x => x.NoLangue);
                });

            migrationBuilder.CreateTable(
                name: "SousTitres",
                columns: table => new
                {
                    NoSousTitre = table.Column<int>(type: "int", nullable: false),
                    LangueSousTitre = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SousTitres", x => x.NoSousTitre);
                });

            migrationBuilder.CreateTable(
                name: "Supplements",
                columns: table => new
                {
                    NoSupplement = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplements", x => x.NoSupplement);
                });

            migrationBuilder.CreateTable(
                name: "FilmsActeurs",
                columns: table => new
                {
                    NoFilm = table.Column<int>(type: "int", nullable: false),
                    NoActeur = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmsActeurs", x => new { x.NoFilm, x.NoActeur });
                    table.ForeignKey(
                        name: "FK_FilmsActeurs_Acteurs_NoActeur",
                        column: x => x.NoActeur,
                        principalTable: "Acteurs",
                        principalColumn: "NoActeur",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilmsActeurs_Films_NoFilm",
                        column: x => x.NoFilm,
                        principalTable: "Films",
                        principalColumn: "NoFilm",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmsLangues",
                columns: table => new
                {
                    NoFilm = table.Column<int>(type: "int", nullable: false),
                    NoLangue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmsLangues", x => new { x.NoFilm, x.NoLangue });
                    table.ForeignKey(
                        name: "FK_FilmsLangues_Films_NoFilm",
                        column: x => x.NoFilm,
                        principalTable: "Films",
                        principalColumn: "NoFilm",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmsLangues_Langues_NoLangue",
                        column: x => x.NoLangue,
                        principalTable: "Langues",
                        principalColumn: "NoLangue",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilmsSousTitres",
                columns: table => new
                {
                    NoFilm = table.Column<int>(type: "int", nullable: false),
                    NoSousTitre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmsSousTitres", x => new { x.NoFilm, x.NoSousTitre });
                    table.ForeignKey(
                        name: "FK_FilmsSousTitres_Films_NoFilm",
                        column: x => x.NoFilm,
                        principalTable: "Films",
                        principalColumn: "NoFilm",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmsSousTitres_SousTitres_NoSousTitre",
                        column: x => x.NoSousTitre,
                        principalTable: "SousTitres",
                        principalColumn: "NoSousTitre",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilmsSupplements",
                columns: table => new
                {
                    NoFilm = table.Column<int>(type: "int", nullable: false),
                    NoSupplement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmsSupplements", x => new { x.NoFilm, x.NoSupplement });
                    table.ForeignKey(
                        name: "FK_FilmsSupplements_Films_NoFilm",
                        column: x => x.NoFilm,
                        principalTable: "Films",
                        principalColumn: "NoFilm",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmsSupplements_Supplements_NoSupplement",
                        column: x => x.NoSupplement,
                        principalTable: "Supplements",
                        principalColumn: "NoSupplement",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmsActeurs_NoActeur",
                table: "FilmsActeurs",
                column: "NoActeur");

            migrationBuilder.CreateIndex(
                name: "IX_FilmsLangues_NoLangue",
                table: "FilmsLangues",
                column: "NoLangue");

            migrationBuilder.CreateIndex(
                name: "IX_FilmsSousTitres_NoSousTitre",
                table: "FilmsSousTitres",
                column: "NoSousTitre");

            migrationBuilder.CreateIndex(
                name: "IX_FilmsSupplements_NoSupplement",
                table: "FilmsSupplements",
                column: "NoSupplement");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmsActeurs");

            migrationBuilder.DropTable(
                name: "FilmsLangues");

            migrationBuilder.DropTable(
                name: "FilmsSousTitres");

            migrationBuilder.DropTable(
                name: "FilmsSupplements");

            migrationBuilder.DropTable(
                name: "Acteurs");

            migrationBuilder.DropTable(
                name: "Langues");

            migrationBuilder.DropTable(
                name: "SousTitres");

            migrationBuilder.DropTable(
                name: "Supplements");
        }
    }
}
