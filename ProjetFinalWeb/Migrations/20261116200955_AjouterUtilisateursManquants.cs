using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjetFinalWeb.Migrations
{
    /// <inheritdoc />
    public partial class AjouterUtilisateursManquants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "UtilisateursPreferences",
                columns: table => new
                {
                    NoUtilisateur = table.Column<int>(type: "int", nullable: false),
                    NoPreference = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilisateursPreferences", x => new { x.NoUtilisateur, x.NoPreference });
                    table.ForeignKey(
                        name: "FK_UtilisateursPreferences_Preferences_NoPreference",
                        column: x => x.NoPreference,
                        principalTable: "Preferences",
                        principalColumn: "NoPreference",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtilisateursPreferences_Utilisateurs_NoUtilisateur",
                        column: x => x.NoUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "NoUtilisateur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "NoUtilisateur", "Courriel", "MotPasse", "NomUtilisateur", "TypeUtilisateur" },
                values: new object[,]
                {
                    { 3, "helene@gmail.com", 11111, "helene", "S" },
                    { 4, "lucille@gmail.com", 22222, "lucille", "U" },
                    { 5, "louis.marie@gmail.com", 33333, "louis", "U" },
                    { 6, "rejean@gmail.com", 44444, "rejean", "U" },
                    { 7, "christian@gmail.com", 55555, "christian", "U" }
                });

            migrationBuilder.InsertData(
                table: "UtilisateursPreferences",
                columns: new[] { "NoPreference", "NoUtilisateur" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 4, 5 },
                    { 3, 6 },
                    { 4, 6 },
                    { 5, 6 },
                    { 1, 7 },
                    { 2, 7 },
                    { 3, 7 },
                    { 4, 7 },
                    { 5, 7 },
                    { 7, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateursPreferences_NoPreference",
                table: "UtilisateursPreferences",
                column: "NoPreference");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UtilisateursPreferences");

            
        }
    }
}
