using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetFinalWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreerTableUtilisateurs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    NoUtilisateur = table.Column<int>(type: "int", nullable: false),
                    NomUtilisateur = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Courriel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MotPasse = table.Column<int>(type: "int", nullable: false),
                    TypeUtilisateur = table.Column<string>(type: "nchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.NoUtilisateur);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_TypesUtilisateur_TypeUtilisateur",
                        column: x => x.TypeUtilisateur,
                        principalTable: "TypesUtilisateur",
                        principalColumn: "TypeUtilisateur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_TypeUtilisateur",
                table: "Utilisateurs",
                column: "TypeUtilisateur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
