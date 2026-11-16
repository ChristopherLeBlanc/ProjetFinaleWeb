
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ProjetFinalWeb.Models;
using ProjetFinalWeb.ViewModels;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly FilmsDbContext _db;

    public AccountController(FilmsDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(
        LoginViewModel model,
        string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        Utilisateur? utilisateur = await _db.Utilisateurs
            .FirstOrDefaultAsync(u =>
                u.NomUtilisateur == model.Identifiant ||
                u.Courriel == model.Identifiant);

        if (utilisateur == null ||
            utilisateur.MotPasse.ToString() != model.MotDePasse)
        {
            ModelState.AddModelError("",
                "Identifiant ou mot de passe incorrect.");

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        var role = utilisateur.TypeUtilisateur switch
        {
            "A" => "Administrateur",
            "S" => "SuperUtilisateur",
            "U" => "Utilisateur",
            _ => "Utilisateur"
        };

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,
                utilisateur.NoUtilisateur.ToString()),

            new Claim(ClaimTypes.Name,
                utilisateur.NomUtilisateur),

            new Claim(ClaimTypes.Email,
                utilisateur.Courriel),

            new Claim(ClaimTypes.Role, role)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        if (!string.IsNullOrEmpty(returnUrl) &&
            Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login", "Account");
    }
}
