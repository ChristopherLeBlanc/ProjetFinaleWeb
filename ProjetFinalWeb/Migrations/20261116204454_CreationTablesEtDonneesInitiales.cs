
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace ProjetFinalWeb.Migrations
{
    public partial class CreationTablesEtDonneesInitiales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Création de la table ValeursPreferences
            migrationBuilder.CreateTable(
                name: "ValeursPreferences",
                columns: table => new
                {
                    NoUtilisateur = table.Column<int>(type: "int", nullable: false),
                    NoPreference = table.Column<int>(type: "int", nullable: false),
                    Valeur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_ValeursPreferences",
                        x => new { x.NoUtilisateur, x.NoPreference });

                    table.ForeignKey(
                        name: "FK_ValeursPreferences_Preferences_NoPreference",
                        column: x => x.NoPreference,
                        principalTable: "Preferences",
                        principalColumn: "NoPreference",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_ValeursPreferences_Utilisateurs_NoUtilisateur",
                        column: x => x.NoUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "NoUtilisateur",
                        onDelete: ReferentialAction.Cascade);
                });

            // Insertion des valeurs initiales
            migrationBuilder.InsertData(
                table: "ValeursPreferences",
                columns: new[] { "NoPreference", "NoUtilisateur", "Valeur" },
                values: new object[,]
                {
                    { 1, 3, "Yellow" },
                    { 2, 3, "Blue" },
                    { 1, 7, "Blue" },
                    { 2, 7, "Yellow" },
                    { 7, 7, "20" }
                });

            // Création de l'index
            migrationBuilder.CreateIndex(
                name: "IX_ValeursPreferences_NoPreference",
                table: "ValeursPreferences",
                column: "NoPreference");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValeursPreferences");
        }
    }
}
