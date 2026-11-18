using Microsoft.EntityFrameworkCore;

namespace ProjetFinalWeb.Models
{
    public partial class FilmsDbContext
    {
        public DbSet<Film> Films { get; set; } = null!;
        public DbSet<Categorie> Categories { get; set; } = null!;
        public DbSet<Format> Formats { get; set; } = null!;
        public DbSet<Producteur> Producteurs { get; set; } = null!;
        public DbSet<Realisateur> Realisateurs { get; set; } = null!;

        private static void ConfigurerFilms(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(f => f.NoFilm);
                entity.Property(f => f.NoFilm).ValueGeneratedNever();
                entity.HasIndex(f => f.TitreFrancais).IsUnique();

                entity.HasOne(f => f.CategorieInfo)
                    .WithMany(c => c.Films)
                    .HasForeignKey(f => f.Categorie)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.FormatInfo)
                    .WithMany(fr => fr.Films)
                    .HasForeignKey(f => f.Format)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Producteur)
                    .WithMany(p => p.Films)
                    .HasForeignKey(f => f.NoProducteur)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Realisateur)
                    .WithMany(r => r.Films)
                    .HasForeignKey(f => f.NoRealisateur)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.UtilisateurMAJ)
                    .WithMany()
                    .HasForeignKey(f => f.NoUtilisateurMAJ)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Categorie>().HasData(
                new Categorie { NoCategorie = 1, Description = "Action" },
                new Categorie { NoCategorie = 2, Description = "Adolescent" },
                new Categorie { NoCategorie = 3, Description = "Biographie" },
                new Categorie { NoCategorie = 4, Description = "Cape et d'épée" },
                new Categorie { NoCategorie = 5, Description = "Catastrophe" },
                new Categorie { NoCategorie = 6, Description = "Chronique" },
                new Categorie { NoCategorie = 7, Description = "Comédie de moeurs" },
                new Categorie { NoCategorie = 8, Description = "Comédie d'horreur" },
                new Categorie { NoCategorie = 9, Description = "Comédie dramatique" },
                new Categorie { NoCategorie = 10, Description = "Comédie fantaisiste" },
                new Categorie { NoCategorie = 11, Description = "Comédie musicale" },
                new Categorie { NoCategorie = 12, Description = "Comédie policière" },
                new Categorie { NoCategorie = 13, Description = "Comédie satirique" },
                new Categorie { NoCategorie = 14, Description = "Comédie sentimentale" },
                new Categorie { NoCategorie = 15, Description = "Comédie" },
                new Categorie { NoCategorie = 16, Description = "Criminel" },
                new Categorie { NoCategorie = 17, Description = "Danse" },
                new Categorie { NoCategorie = 18, Description = "Dessins animés" },
                new Categorie { NoCategorie = 19, Description = "Documentaire" },
                new Categorie { NoCategorie = 20, Description = "Drame de guerre" },
                new Categorie { NoCategorie = 21, Description = "Drame de moeurs" },
                new Categorie { NoCategorie = 22, Description = "Drame d'horreur" },
                new Categorie { NoCategorie = 23, Description = "Drame judiciaire" },
                new Categorie { NoCategorie = 24, Description = "Drame musical" },
                new Categorie { NoCategorie = 25, Description = "Drame poétique" },
                new Categorie { NoCategorie = 26, Description = "Drame policier" },
                new Categorie { NoCategorie = 27, Description = "Drame psychologique" },
                new Categorie { NoCategorie = 28, Description = "Drame sentimental" },
                new Categorie { NoCategorie = 29, Description = "Drame social" },
                new Categorie { NoCategorie = 30, Description = "Drame" },
                new Categorie { NoCategorie = 31, Description = "Espionnage" },
                new Categorie { NoCategorie = 32, Description = "Expérimental" },
                new Categorie { NoCategorie = 33, Description = "Fantastique" },
                new Categorie { NoCategorie = 34, Description = "Film à sketches" },
                new Categorie { NoCategorie = 35, Description = "Film d'animation" },
                new Categorie { NoCategorie = 36, Description = "Film d'art martial" },
                new Categorie { NoCategorie = 37, Description = "Historique" },
                new Categorie { NoCategorie = 38, Description = "Horreur" },
                new Categorie { NoCategorie = 39, Description = "Humoristique" },
                new Categorie { NoCategorie = 40, Description = "Marionnettes" },
                new Categorie { NoCategorie = 41, Description = "Mélodrame" },
                new Categorie { NoCategorie = 42, Description = "Musical" },
                new Categorie { NoCategorie = 43, Description = "Road movie" },
                new Categorie { NoCategorie = 44, Description = "Romantique" },
                new Categorie { NoCategorie = 45, Description = "Science-fiction" },
                new Categorie { NoCategorie = 46, Description = "Série policière" },
                new Categorie { NoCategorie = 47, Description = "Série TV" },
                new Categorie { NoCategorie = 48, Description = "Spectacle d'humour" },
                new Categorie { NoCategorie = 49, Description = "Spectacle musical" },
                new Categorie { NoCategorie = 50, Description = "Suspense" },
                new Categorie { NoCategorie = 51, Description = "Western" }
            );

            modelBuilder.Entity<Format>().HasData(
                new Format { NoFormat = 1, Description = "Blu-Ray" },
                new Format { NoFormat = 2, Description = "Normal" },
                new Format { NoFormat = 3, Description = "Panoramique" }
            );

            modelBuilder.Entity<Producteur>().HasData(
                new Producteur { NoProducteur = 1, Nom = "Francis Ford Coppola" },
                new Producteur { NoProducteur = 2, Nom = "Jerry Weintraub" },
                new Producteur { NoProducteur = 3, Nom = "Jim Czarnecki" },
                new Producteur { NoProducteur = 4, Nom = "Michael Wimer" },
                new Producteur { NoProducteur = 5, Nom = "Pierre Even" },
                new Producteur { NoProducteur = 6, Nom = "Edward S. Feldman" },
                new Producteur { NoProducteur = 7, Nom = "Nicole Robert" },
                new Producteur { NoProducteur = 8, Nom = "Pierre Gendron" },
                new Producteur { NoProducteur = 9, Nom = "Xavier Dolan" },
                new Producteur { NoProducteur = 10, Nom = "Albert S. Ruddy" },
                new Producteur { NoProducteur = 11, Nom = "Henry Fonda" },
                new Producteur { NoProducteur = 12, Nom = "George Clooney" },
                new Producteur { NoProducteur = 13, Nom = "Niki Marvin" },
                new Producteur { NoProducteur = 14, Nom = "Peter Jackson" },
                new Producteur { NoProducteur = 15, Nom = "Stephen J. Cannell" },
                new Producteur { NoProducteur = 16, Nom = "Oren Peli" },
                new Producteur { NoProducteur = 17, Nom = "Matthew Gross" }
            );

            modelBuilder.Entity<Realisateur>().HasData(
                new Realisateur { NoRealisateur = 1, Nom = "Francis Ford Coppola" },
                new Realisateur { NoRealisateur = 2, Nom = "Claude Desrosiers" },
                new Realisateur { NoRealisateur = 3, Nom = "Steven Soderbergh" },
                new Realisateur { NoRealisateur = 4, Nom = "Michael Moore" },
                new Realisateur { NoRealisateur = 5, Nom = "Roland Emmerich" },
                new Realisateur { NoRealisateur = 6, Nom = "Jean-Marc Vallée" },
                new Realisateur { NoRealisateur = 7, Nom = "Kevin Lima" },
                new Realisateur { NoRealisateur = 8, Nom = "Yves Pelletier" },
                new Realisateur { NoRealisateur = 9, Nom = "Éric Canuel" },
                new Realisateur { NoRealisateur = 10, Nom = "Xavier Dolan" },
                new Realisateur { NoRealisateur = 11, Nom = "Sidney Lumet" },
                new Realisateur { NoRealisateur = 12, Nom = "John Maybury" },
                new Realisateur { NoRealisateur = 13, Nom = "Frank Darabont" },
                new Realisateur { NoRealisateur = 14, Nom = "Andy Tennant" },
                new Realisateur { NoRealisateur = 15, Nom = "Joe Carnahan" },
                new Realisateur { NoRealisateur = 16, Nom = "Oren Peli" },
                new Realisateur { NoRealisateur = 17, Nom = "Philippe Gagnon" },
                new Realisateur { NoRealisateur = 18, Nom = "Julie Taymor" }
            );
        }
    }
}
