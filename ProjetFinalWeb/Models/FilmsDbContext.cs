
using Microsoft.EntityFrameworkCore;

namespace ProjetFinalWeb.Models
{
    public class FilmsDbContext : DbContext
    {
        public FilmsDbContext(DbContextOptions<FilmsDbContext> options)
            : base(options)
        {
        }

        // =========================================
        // TABLES
        // =========================================

        public DbSet<Utilisateur> Utilisateurs { get; set; }

        public DbSet<TypeUtilisateur> TypesUtilisateur { get; set; }

        public DbSet<Preference> Preferences { get; set; }

        public DbSet<UtilisateursPreference> UtilisateursPreferences { get; set; }

        public DbSet<ValeurPreference> ValeursPreferences { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --
            // utilisateur
            // --

            modelBuilder.Entity<Utilisateur>()
                .HasKey(u => u.NoUtilisateur);

            modelBuilder.Entity<Utilisateur>()
                .HasOne(u => u.Type)
                .WithMany(t => t.Utilisateurs)
                .HasForeignKey(u => u.TypeUtilisateur);


            modelBuilder.Entity<Utilisateur>().HasData(
                new Utilisateur
                {
                    NoUtilisateur = 3,
                    NomUtilisateur = "helene",
                    Courriel = "helene@gmail.com",
                    MotPasse = 11111,
                    TypeUtilisateur = "S"
                },
                new Utilisateur
                {
                    NoUtilisateur = 4,
                    NomUtilisateur = "lucille",
                    Courriel = "lucille@gmail.com",
                    MotPasse = 22222,
                    TypeUtilisateur = "U"
                },
                new Utilisateur
                {
                    NoUtilisateur = 5,
                    NomUtilisateur = "louis",
                    Courriel = "louis.marie@gmail.com",
                    MotPasse = 33333,
                    TypeUtilisateur = "U"
                },
                new Utilisateur
                {
                    NoUtilisateur = 6,
                    NomUtilisateur = "rejean",
                    Courriel = "rejean@gmail.com",
                    MotPasse = 44444,
                    TypeUtilisateur = "U"
                },
                new Utilisateur
                {
                    NoUtilisateur = 7,
                    NomUtilisateur = "christian",
                    Courriel = "christian@gmail.com",
                    MotPasse = 55555,
                    TypeUtilisateur = "U"
                }
            );


            // -- 
            // TypesUtilisateur
            // --

            modelBuilder.Entity<TypeUtilisateur>()
                .HasKey(t => t.Type);


            // -- 
            // preference
            // --

            modelBuilder.Entity<Preference>()
                .HasKey(p => p.NoPreference);

            modelBuilder.Entity<Preference>().HasData(
                new Preference
                {
                    NoPreference = 1,
                    Description = "Couleur de fond"
                },
                new Preference
                {
                    NoPreference = 2,
                    Description = "Couleur du texte"
                },
                new Preference
                {
                    NoPreference = 3,
                    Description = "Envoi courriel si ajout"
                },
                new Preference
                {
                    NoPreference = 4,
                    Description = "Envoi courriel si appropriation"
                },
                new Preference
                {
                    NoPreference = 5,
                    Description = "Envoi courriel si suppression"
                },
                new Preference
                {
                    NoPreference = 6,
                    Description = "Image de fond"
                },
                new Preference
                {
                    NoPreference = 7,
                    Description = "Nombre de films par page"
                }
            );


            // --
            // UtilisateursPreference
            // --

            modelBuilder.Entity<UtilisateursPreference>()
                .HasKey(up => new { up.NoUtilisateur, up.NoPreference });

            modelBuilder.Entity<UtilisateursPreference>()
                .HasOne(up => up.Utilisateur)
                .WithMany()
                .HasForeignKey(up => up.NoUtilisateur);

            modelBuilder.Entity<UtilisateursPreference>()
                .HasOne(up => up.Preference)
                .WithMany()
                .HasForeignKey(up => up.NoPreference);


            modelBuilder.Entity<UtilisateursPreference>().HasData(
                new UtilisateursPreference { NoUtilisateur = 3, NoPreference = 1 },
                new UtilisateursPreference { NoUtilisateur = 3, NoPreference = 2 },
                new UtilisateursPreference { NoUtilisateur = 4, NoPreference = 3 },
                new UtilisateursPreference { NoUtilisateur = 5, NoPreference = 3 },
                new UtilisateursPreference { NoUtilisateur = 5, NoPreference = 4 },
                new UtilisateursPreference { NoUtilisateur = 6, NoPreference = 3 },
                new UtilisateursPreference { NoUtilisateur = 6, NoPreference = 4 },
                new UtilisateursPreference { NoUtilisateur = 6, NoPreference = 5 },
                new UtilisateursPreference { NoUtilisateur = 7, NoPreference = 1 },
                new UtilisateursPreference { NoUtilisateur = 7, NoPreference = 2 },
                new UtilisateursPreference { NoUtilisateur = 7, NoPreference = 3 },
                new UtilisateursPreference { NoUtilisateur = 7, NoPreference = 4 },
                new UtilisateursPreference { NoUtilisateur = 7, NoPreference = 5 },
                new UtilisateursPreference { NoUtilisateur = 7, NoPreference = 7 }
            );


            // -- 
            // ValeurPreference
            // --

            modelBuilder.Entity<ValeurPreference>()
                .HasKey(vp => new { vp.NoUtilisateur, vp.NoPreference });

            modelBuilder.Entity<ValeurPreference>()
                .HasOne(vp => vp.Utilisateur)
                .WithMany()
                .HasForeignKey(vp => vp.NoUtilisateur);

            modelBuilder.Entity<ValeurPreference>()
                .HasOne(vp => vp.Preference)
                .WithMany()
                .HasForeignKey(vp => vp.NoPreference);


            modelBuilder.Entity<ValeurPreference>().HasData(
                new ValeurPreference
                {
                    NoUtilisateur = 3,
                    NoPreference = 1,
                    Valeur = "Yellow"
                },
                new ValeurPreference
                {
                    NoUtilisateur = 3,
                    NoPreference = 2,
                    Valeur = "Blue"
                },
                new ValeurPreference
                {
                    NoUtilisateur = 7,
                    NoPreference = 1,
                    Valeur = "Blue"
                },
                new ValeurPreference
                {
                    NoUtilisateur = 7,
                    NoPreference = 2,
                    Valeur = "Yellow"
                },
                new ValeurPreference
                {
                    NoUtilisateur = 7,
                    NoPreference = 7,
                    Valeur = "20"
                }
            );
        }
    }
}
