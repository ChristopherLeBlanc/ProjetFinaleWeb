using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetFinalWeb.Migrations
{
    /// <inheritdoc />
    public partial class TablesFilms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    NoCategorie = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.NoCategorie);
                });

            migrationBuilder.CreateTable(
                name: "Exemplaires",
                columns: table => new
                {
                    NoExemplaire = table.Column<int>(type: "int", nullable: false),
                    NoUtilisateurProprietaire = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exemplaires", x => x.NoExemplaire);
                    table.ForeignKey(
                        name: "FK_Exemplaires_Utilisateurs_NoUtilisateurProprietaire",
                        column: x => x.NoUtilisateurProprietaire,
                        principalTable: "Utilisateurs",
                        principalColumn: "NoUtilisateur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Formats",
                columns: table => new
                {
                    NoFormat = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formats", x => x.NoFormat);
                });

            migrationBuilder.CreateTable(
                name: "Producteurs",
                columns: table => new
                {
                    NoProducteur = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producteurs", x => x.NoProducteur);
                });

            migrationBuilder.CreateTable(
                name: "Realisateurs",
                columns: table => new
                {
                    NoRealisateur = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realisateurs", x => x.NoRealisateur);
                });

            migrationBuilder.CreateTable(
                name: "EmpruntsFilms",
                columns: table => new
                {
                    NoExemplaire = table.Column<int>(type: "int", nullable: false),
                    NoUtilisateur = table.Column<int>(type: "int", nullable: false),
                    DateEmprunt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpruntsFilms", x => new { x.NoExemplaire, x.NoUtilisateur });
                    table.ForeignKey(
                        name: "FK_EmpruntsFilms_Exemplaires_NoExemplaire",
                        column: x => x.NoExemplaire,
                        principalTable: "Exemplaires",
                        principalColumn: "NoExemplaire",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpruntsFilms_Utilisateurs_NoUtilisateur",
                        column: x => x.NoUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "NoUtilisateur",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    NoFilm = table.Column<int>(type: "int", nullable: false),
                    AnneeSortie = table.Column<int>(type: "int", nullable: true),
                    Categorie = table.Column<int>(type: "int", nullable: true),
                    Format = table.Column<int>(type: "int", nullable: true),
                    DateMAJ = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoUtilisateurMAJ = table.Column<int>(type: "int", nullable: true),
                    Resume = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DureeMinutes = table.Column<int>(type: "int", nullable: true),
                    FilmOriginal = table.Column<bool>(type: "bit", nullable: true),
                    ImagePochette = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NbDisques = table.Column<int>(type: "int", nullable: true),
                    TitreFrancais = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TitreOriginal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VersionEtendue = table.Column<bool>(type: "bit", nullable: true),
                    NoRealisateur = table.Column<int>(type: "int", nullable: true),
                    NoProducteur = table.Column<int>(type: "int", nullable: true),
                    Xtra = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.NoFilm);
                    table.ForeignKey(
                        name: "FK_Films_Categories_Categorie",
                        column: x => x.Categorie,
                        principalTable: "Categories",
                        principalColumn: "NoCategorie",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Films_Formats_Format",
                        column: x => x.Format,
                        principalTable: "Formats",
                        principalColumn: "NoFormat",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Films_Producteurs_NoProducteur",
                        column: x => x.NoProducteur,
                        principalTable: "Producteurs",
                        principalColumn: "NoProducteur",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Films_Realisateurs_NoRealisateur",
                        column: x => x.NoRealisateur,
                        principalTable: "Realisateurs",
                        principalColumn: "NoRealisateur",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Films_Utilisateurs_NoUtilisateurMAJ",
                        column: x => x.NoUtilisateurMAJ,
                        principalTable: "Utilisateurs",
                        principalColumn: "NoUtilisateur",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpruntsFilms_NoUtilisateur",
                table: "EmpruntsFilms",
                column: "NoUtilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Exemplaires_NoUtilisateurProprietaire",
                table: "Exemplaires",
                column: "NoUtilisateurProprietaire");

            migrationBuilder.CreateIndex(
                name: "IX_Films_Categorie",
                table: "Films",
                column: "Categorie");

            migrationBuilder.CreateIndex(
                name: "IX_Films_Format",
                table: "Films",
                column: "Format");

            migrationBuilder.CreateIndex(
                name: "IX_Films_NoProducteur",
                table: "Films",
                column: "NoProducteur");

            migrationBuilder.CreateIndex(
                name: "IX_Films_NoRealisateur",
                table: "Films",
                column: "NoRealisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Films_NoUtilisateurMAJ",
                table: "Films",
                column: "NoUtilisateurMAJ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpruntsFilms");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Exemplaires");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Formats");

            migrationBuilder.DropTable(
                name: "Producteurs");

            migrationBuilder.DropTable(
                name: "Realisateurs");
        }
    }
}
