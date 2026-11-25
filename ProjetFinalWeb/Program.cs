
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetFinalWeb.Models;

var builder = WebApplication.CreateBuilder(args);


// BD

builder.Services.AddDbContext<FilmsDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// identity

builder.Services.AddIdentity<Utilisateur, IdentityRole<int>>(options =>
{
    // Les mots de passe doivent contenir au moins 5 caractères.
    options.Password.RequiredLength = 5;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 1;

    // Chaque utilisateur doit avoir un courriel unique.
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<FilmsDbContext>()
.AddDefaultTokenProviders();


// cookies identity

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});

// autorisation

builder.Services.AddAuthorization(options =>
{
    // Toutes les pages nécessitent une connexion,
    // sauf celles marquées [AllowAnonymous].
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});


//MVC

builder.Services.AddControllersWithViews();


var app = builder.Build();
// creation compte admin

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<Utilisateur>>();

    var admin = await userManager.FindByNameAsync("admin");

    if (admin == null)
    {
        var nouvelAdmin = new Utilisateur
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            TypeUtilisateur = "A",
            EmailConfirmed = true
        };

        var resultat = await userManager.CreateAsync(
            nouvelAdmin,
            "99999"
        );

        if (!resultat.Succeeded)
        {
            throw new Exception(
                string.Join(" | ",
                    resultat.Errors.Select(e => e.Description))
            );
        }
    }
}

// creation des utilisateurs

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<Utilisateur>>();

    var utilisateurs = new List<Utilisateur>
    {
        new Utilisateur
        {
            UserName = "chris",
            Email = "cleblanc.hrs@gmail.com",
            TypeUtilisateur = "S",
            EmailConfirmed = true
        },
        new Utilisateur
        {
            UserName = "lucille",
            Email = "lucille@gmail.com",
            TypeUtilisateur = "U",
            EmailConfirmed = true
        },
        new Utilisateur
        {
            UserName = "louis",
            Email = "louis.marie@gmail.com",
            TypeUtilisateur = "U",
            EmailConfirmed = true
        },
        new Utilisateur
        {
            UserName = "rejean",
            Email = "rejean@gmail.com",
            TypeUtilisateur = "U",
            EmailConfirmed = true
        },
        new Utilisateur
        {
            UserName = "christian",
            Email = "christian@gmail.com",
            TypeUtilisateur = "U",
            EmailConfirmed = true
        },
        new Utilisateur
        {
            UserName = "jose",
            Email = "jose@gmail.com",
            TypeUtilisateur = "U",
            EmailConfirmed = true
        }
    };

    string[] motsDePasse =
    {
        "11111",
        "22222",
        "33333",
        "44444",
        "55555",
        "66666"
    };

    for (int i = 0; i < utilisateurs.Count; i++)
    {
        var existant = await userManager.FindByNameAsync(
            utilisateurs[i].UserName!
        );

        if (existant == null)
        {
            var resultat = await userManager.CreateAsync(
                utilisateurs[i],
                motsDePasse[i]
            );

            if (!resultat.Succeeded)
            {
                throw new Exception(
                    string.Join(" | ",
                        resultat.Errors.Select(e => e.Description))
                );
            }
        }
    }
}


// config de l'app

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


// routes

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
