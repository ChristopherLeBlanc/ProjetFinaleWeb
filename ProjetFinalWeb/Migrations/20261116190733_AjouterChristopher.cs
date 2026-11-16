using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetFinalWeb.Migrations
{
    /// <inheritdoc />
    public partial class AjouterChristopher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "NoUtilisateur", "Courriel", "MotPasse", "NomUtilisateur", "TypeUtilisateur" },
                values: new object[] { 2, "cleblanc.hrs@gmail.com", 12345, "chris", "A" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "NoUtilisateur",
                keyValue: 2);
        }
    }
}
