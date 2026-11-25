using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using static System.Net.WebRequestMethods;

namespace ProjetFinalWeb.Models
{
    public class FilmsDbContext :
        IdentityDbContext<Utilisateur, IdentityRole<int>, int>
    {
        public FilmsDbContext(DbContextOptions<FilmsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<TypeUtilisateur> TypesUtilisateur { get; set; }
        public DbSet<Exemplaire> Exemplaires { get; set; }
        public DbSet<EmpruntFilm> EmpruntsFilms { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<UtilisateursPreference> UtilisateursPreferences { get; set; }
        public DbSet<ValeurPreference> ValeursPreferences { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<Producteur> Producteurs { get; set; }
        public DbSet<Realisateur> Realisateurs { get; set; }

        public DbSet<Acteur> Acteurs { get; set; }
        public DbSet<Langue> Langues { get; set; }
        public DbSet<SousTitre> SousTitres { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<FilmActeur> FilmsActeurs { get; set; }
        public DbSet<FilmLangue> FilmsLangues { get; set; }
        public DbSet<FilmSousTitre> FilmsSousTitres { get; set; }
        public DbSet<FilmSupplement> FilmsSupplements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.ToTable("Utilisateurs");

                entity.Property(u => u.Id)
                    .HasColumnName("NoUtilisateur");

                entity.Property(u => u.UserName)
                    .HasColumnName("NomUtilisateur")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(u => u.NormalizedUserName)
                    .HasMaxLength(10);

                entity.Property(u => u.Email)
                    .HasColumnName("Courriel")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(u => u.NormalizedEmail)
                    .HasMaxLength(50);

                entity.HasOne(u => u.Type)
                    .WithMany(t => t.Utilisateurs)
                    .HasForeignKey(u => u.TypeUtilisateur);
            });

            modelBuilder.Entity<TypeUtilisateur>(entity =>
            {
                entity.ToTable("TypesUtilisateur");
                entity.HasKey(t => t.Type);
            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.ToTable("Preferences");
                entity.HasKey(p => p.NoPreference);
            });

            modelBuilder.Entity<UtilisateursPreference>(entity =>
            {
                entity.ToTable("UtilisateursPreferences");

                entity.HasKey(up => new
                {
                    up.NoUtilisateur,
                    up.NoPreference
                });

                entity.HasOne(up => up.Utilisateur)
                    .WithMany()
                    .HasForeignKey(up => up.NoUtilisateur);

                entity.HasOne(up => up.Preference)
                    .WithMany()
                    .HasForeignKey(up => up.NoPreference);
            });

            modelBuilder.Entity<ValeurPreference>(entity =>
            {
                entity.ToTable("ValeursPreferences");

                entity.HasKey(vp => new
                {
                    vp.NoUtilisateur,
                    vp.NoPreference
                });

                entity.HasOne(vp => vp.Utilisateur)
                    .WithMany()
                    .HasForeignKey(vp => vp.NoUtilisateur);

                entity.HasOne(vp => vp.Preference)
                    .WithMany()
                    .HasForeignKey(vp => vp.NoPreference);
            });

            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.NoCategorie);
            });

            modelBuilder.Entity<Format>(entity =>
            {
                entity.ToTable("Formats");
                entity.HasKey(f => f.NoFormat);
            });

            modelBuilder.Entity<Producteur>(entity =>
            {
                entity.ToTable("Producteurs");
                entity.HasKey(p => p.NoProducteur);
            });

            modelBuilder.Entity<Realisateur>(entity =>
            {
                entity.ToTable("Realisateurs");
                entity.HasKey(r => r.NoRealisateur);
            });

            modelBuilder.Entity<Acteur>(entity =>
            {
                entity.ToTable("Acteurs", t =>
                    t.HasCheckConstraint("CK_Acteurs_Sexe",
                        "[Sexe] IN (N'F', N'H', N'N')"));
                entity.HasKey(a => a.NoActeur);
            });

            modelBuilder.Entity<Langue>(entity =>
            {
                entity.ToTable("Langues");
                entity.HasKey(l => l.NoLangue);
            });

            modelBuilder.Entity<SousTitre>(entity =>
            {
                entity.ToTable("SousTitres");
                entity.HasKey(s => s.NoSousTitre);
            });

            modelBuilder.Entity<Supplement>(entity =>
            {
                entity.ToTable("Supplements");
                entity.HasKey(s => s.NoSupplement);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("Films");

                entity.HasKey(f => f.NoFilm);

                entity.Property(f => f.NoProducteur)
                    .HasColumnName("Producteur");

                entity.Property(f => f.NoRealisateur)
                    .HasColumnName("Realisateur");

                entity.Property(f => f.NoUtilisateurMAJ)
                    .HasColumnName("UtilisateurMAJ");

                entity.HasOne(f => f.CategorieInfo)
                    .WithMany()
                    .HasForeignKey(f => f.Categorie)
                    .HasPrincipalKey(c => c.NoCategorie)
                    .OnDelete(DeleteBehavior.Restrict);




                entity.HasOne(f => f.FormatInfo)
                    .WithMany()
                    .HasForeignKey(f => f.Format)
                    .HasPrincipalKey(f => f.NoFormat)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Producteur)
                    .WithMany()
                    .HasForeignKey(f => f.NoProducteur)
                    .HasPrincipalKey(p => p.NoProducteur)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Realisateur)
                    .WithMany()
                    .HasForeignKey(f => f.NoRealisateur)
                    .HasPrincipalKey(r => r.NoRealisateur)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.UtilisateurMAJ)
                    .WithMany()
                    .HasForeignKey(f => f.NoUtilisateurMAJ)
                    .HasPrincipalKey(u => u.Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FilmActeur>(entity =>
            {
                entity.ToTable("FilmsActeurs");
                entity.HasKey(fa => new { fa.NoFilm, fa.NoActeur });

                entity.HasOne(fa => fa.Film)
                    .WithMany()
                    .HasForeignKey(fa => fa.NoFilm)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(fa => fa.Acteur)
                    .WithMany(a => a.FilmsActeurs)
                    .HasForeignKey(fa => fa.NoActeur)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FilmLangue>(entity =>
            {
                entity.ToTable("FilmsLangues");
                entity.HasKey(fl => new { fl.NoFilm, fl.NoLangue });

                entity.HasOne(fl => fl.Film)
                    .WithMany()
                    .HasForeignKey(fl => fl.NoFilm)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(fl => fl.Langue)
                    .WithMany(l => l.FilmsLangues)
                    .HasForeignKey(fl => fl.NoLangue)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FilmSousTitre>(entity =>
            {
                entity.ToTable("FilmsSousTitres");
                entity.HasKey(fs => new { fs.NoFilm, fs.NoSousTitre });

                entity.HasOne(fs => fs.Film)
                    .WithMany()
                    .HasForeignKey(fs => fs.NoFilm)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(fs => fs.SousTitre)
                    .WithMany(s => s.FilmsSousTitres)
                    .HasForeignKey(fs => fs.NoSousTitre)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FilmSupplement>(entity =>
            {
                entity.ToTable("FilmsSupplements");
                entity.HasKey(fs => new { fs.NoFilm, fs.NoSupplement });

                entity.HasOne(fs => fs.Film)
                    .WithMany()
                    .HasForeignKey(fs => fs.NoFilm)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(fs => fs.Supplement)
                    .WithMany(s => s.FilmsSupplements)
                    .HasForeignKey(fs => fs.NoSupplement)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Exemplaire>(entity =>
            {
                entity.ToTable("Exemplaires");

                entity.HasKey(e => e.NoExemplaire);

                entity.HasOne(e => e.Proprietaire)
                    .WithMany()
                    .HasForeignKey(e => e.NoUtilisateurProprietaire);
            });

            modelBuilder.Entity<EmpruntFilm>(entity =>
            {
                entity.ToTable("EmpruntsFilms");

                entity.HasKey(e => new
                {
                    e.NoExemplaire,
                    e.NoUtilisateur
                });

                entity.HasOne(e => e.Exemplaire)
                    .WithMany(e => e.Emprunts)
                    .HasForeignKey(e => e.NoExemplaire);

                entity.HasOne(e => e.Utilisateur)
                    .WithMany()
                    .HasForeignKey(e => e.NoUtilisateur)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}