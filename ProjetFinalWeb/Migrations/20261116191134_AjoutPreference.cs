using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjetFinalWeb.Migrations
{
    /// <inheritdoc />
    public partial class AjoutPreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    NoPreference = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.NoPreference);
                });

            migrationBuilder.InsertData(
                table: "Preferences",
                columns: new[] { "NoPreference", "Description" },
                values: new object[,]
                {
                    { 1, "Couleur de fond" },
                    { 2, "Couleur du texte" },
                    { 3, "Envoi courriel si ajout" },
                    { 4, "Envoi courriel si appropriation" },
                    { 5, "Envoi courriel si suppression" },
                    { 6, "Image de fond" },
                    { 7, "Nombre de films par page" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Preferences");

            
        }
    }
}
