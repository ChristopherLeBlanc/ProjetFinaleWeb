
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinalWeb.Models
{
    public class FilmsDbContext :
        IdentityDbContext<Utilisateur, IdentityRole<int>, int>
    {
        public FilmsDbContext(DbContextOptions<FilmsDbContext> options)
            : base(options)
        {
        }

        // Tables du projet
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<TypeUtilisateur> TypesUtilisateur { get; set; }

        public DbSet<Preference> Preferences { get; set; }

        public DbSet<UtilisateursPreference> UtilisateursPreferences { get; set; }

        public DbSet<ValeurPreference> ValeursPreferences { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // utilisateur

            modelBuilder.Entity<Utilisateur>()
                .ToTable("Utilisateurs");

            modelBuilder.Entity<Utilisateur>()
                .Property(u => u.Id)
                .HasColumnName("NoUtilisateur");

            modelBuilder.Entity<Utilisateur>()
                .Property(u => u.UserName)
                .HasColumnName("NomUtilisateur")
                .HasMaxLength(10)
                .IsRequired();

            modelBuilder.Entity<Utilisateur>()
                .Property(u => u.NormalizedUserName)
                .HasMaxLength(10);

            modelBuilder.Entity<Utilisateur>()
                .Property(u => u.Email)
                .HasColumnName("Courriel")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Utilisateur>()
                .Property(u => u.NormalizedEmail)
                .HasMaxLength(50);

            // Relation Utilisateur - TypeUtilisateur
            modelBuilder.Entity<Utilisateur>()
                .HasOne(u => u.Type)
                .WithMany(t => t.Utilisateurs)
                .HasForeignKey(u => u.TypeUtilisateur);


            // type utilisateur

            modelBuilder.Entity<TypeUtilisateur>()
                .ToTable("TypesUtilisateur");

            modelBuilder.Entity<TypeUtilisateur>()
                .HasKey(t => t.Type);


            // preference

            modelBuilder.Entity<Preference>()
                .ToTable("Preferences");

            modelBuilder.Entity<Preference>()
                .HasKey(p => p.NoPreference);


            // utilisateur preference

            modelBuilder.Entity<UtilisateursPreference>()
                .ToTable("UtilisateursPreferences");

            modelBuilder.Entity<UtilisateursPreference>()
                .HasKey(up => new
                {
                    up.NoUtilisateur,
                    up.NoPreference
                });

            modelBuilder.Entity<UtilisateursPreference>()
                .HasOne(up => up.Utilisateur)
                .WithMany()
                .HasForeignKey(up => up.NoUtilisateur);

            modelBuilder.Entity<UtilisateursPreference>()
                .HasOne(up => up.Preference)
                .WithMany()
                .HasForeignKey(up => up.NoPreference);


            // valeur preference

            modelBuilder.Entity<ValeurPreference>()
                .ToTable("ValeursPreferences");

            modelBuilder.Entity<ValeurPreference>()
                .HasKey(vp => new
                {
                    vp.NoUtilisateur,
                    vp.NoPreference
                });

            modelBuilder.Entity<ValeurPreference>()
                .HasOne(vp => vp.Utilisateur)
                .WithMany()
                .HasForeignKey(vp => vp.NoUtilisateur);

            modelBuilder.Entity<ValeurPreference>()
                .HasOne(vp => vp.Preference)
                .WithMany()
                .HasForeignKey(vp => vp.NoPreference);
        }
    }
}
