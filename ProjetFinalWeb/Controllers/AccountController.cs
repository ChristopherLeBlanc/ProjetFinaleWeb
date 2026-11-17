
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ProjetFinalWeb.Models;
using ProjetFinalWeb.ViewModels;

public class AccountController : Controller
{
    private readonly UserManager<Utilisateur> _userManager;
    private readonly SignInManager<Utilisateur> _signInManager;

    public AccountController(
        UserManager<Utilisateur> userManager,
        SignInManager<Utilisateur> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [AllowAnonymous]
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

        Utilisateur? utilisateur = null;

        string identifiant = model.Identifiant.Trim();

        // verifie le nom d'util
        utilisateur = await _userManager.FindByNameAsync(identifiant);

        // verifie l'email
        if (utilisateur == null)
        {
            utilisateur = await _userManager.FindByEmailAsync(identifiant);
        }

        // verifie que l'util existe
        if (utilisateur == null)
        {
            ModelState.AddModelError("",
                "Identifiant ou mot de passe incorrect.");

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        // checker le mot de passe
        var resultat = await _signInManager.CheckPasswordSignInAsync(
            utilisateur,
            model.MotDePasse,
            lockoutOnFailure: false);

        if (!resultat.Succeeded)
        {
            ModelState.AddModelError("",
                "Identifiant ou mot de passe incorrect.");

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        // Déterminer le type
        string role = utilisateur.TypeUtilisateur switch
        {
            "A" => "Administrateur",
            "S" => "SuperUtilisateur",
            "U" => "Utilisateur",
            _ => "Utilisateur"
        };

        // Ajouter le rôle
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, role)
        };

        // Connecter l'utilisateur avec Identity
        await _signInManager.SignInWithClaimsAsync(
            utilisateur,
            isPersistent: false,
            claims);

        // Redirection
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
        await _signInManager.SignOutAsync();

        return RedirectToAction("Login", "Account");
    }
}
