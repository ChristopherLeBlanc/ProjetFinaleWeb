using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetFinalWeb.Models;
using ProjetFinalWeb.ViewModels;

namespace ProjetFinalWeb.Controllers
{
    public class FilmsController : Controller
    {
        private const int NoPreferenceTaillePage = 7;
        private const int TaillePageParDefaut = 12;

        private const int FacteurExemplaire = 100;

        private readonly FilmsDbContext _db;

        public FilmsController(FilmsDbContext db)
        {
            _db = db;
        }


        // CE QUI AFFICHE LA LISTE DES DVDS
        [HttpGet]
        public async Task<IActionResult> Index(string tri = "titre", string? recherche = null, int page = 1)
        {
            int noUtilisateur = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            int taillePage = await ObtenirTaillePageAsync(noUtilisateur);

            var exemplaires =
                from e in _db.Exemplaires
                join f in _db.Films on e.NoExemplaire / FacteurExemplaire equals f.NoFilm
                select new
                {
                    e.NoExemplaire,
                    f.Titre,
                    f.TitreOriginal,
                    f.ImagePochette,
                    NoDetenteur = e.Emprunts
                        .OrderByDescending(x => x.DateEmprunt)
                        .Select(x => (int?)x.NoUtilisateur)
                        .FirstOrDefault() ?? e.NoUtilisateurProprietaire
                };

            var requete =
                from x in exemplaires
                join u in _db.Utilisateurs on x.NoDetenteur equals u.Id
                select new
                {
                    x.NoExemplaire,
                    x.Titre,
                    x.TitreOriginal,
                    x.ImagePochette,
                    x.NoDetenteur,
                    NomDetenteur = u.UserName ?? ""
                };

            if (!string.IsNullOrWhiteSpace(recherche))
            {
                string r = recherche.Trim();
                requete = requete.Where(x => x.Titre.Contains(r)
                                          || (x.TitreOriginal != null && x.TitreOriginal.Contains(r)));
            }

            requete = tri switch
            {
                "nom"       => requete.OrderBy(x => x.NomDetenteur).ThenBy(x => x.NoExemplaire),
                "nom-titre" => requete.OrderBy(x => x.NomDetenteur).ThenBy(x => x.Titre),
                _           => requete.OrderBy(x => x.Titre).ThenBy(x => x.NoExemplaire)
            };
            if (tri != "nom" && tri != "nom-titre") tri = "titre";

            int total = await requete.CountAsync();
            int nbPages = Math.Max(1, (int)Math.Ceiling(total / (double)taillePage));
            page = Math.Clamp(page, 1, nbPages);

            var films = await requete
                .Skip((page - 1) * taillePage)
                .Take(taillePage)
                .Select(x => new FilmListeItem
                {
                    NoExemplaire = x.NoExemplaire,
                    Titre = x.Titre,
                    ImagePochette = x.ImagePochette,
                    NomEmprunteur = x.NomDetenteur,
                    EstEnMain = x.NoDetenteur == noUtilisateur
                })
                .ToListAsync();

            for (int i = 0; i < films.Count; i++)
                films[i].Numero = (page - 1) * taillePage + i + 1;

            return View(new FilmsListeViewModel
            {
                Films = films,
                Page = page,
                NbPages = nbPages,
                TaillePage = taillePage,
                Total = total,
                Tri = tri,
                Recherche = recherche
            });
        }

        private async Task<int> ObtenirTaillePageAsync(int noUtilisateur)
        {
            string? valeur = await _db.ValeursPreferences
                .Where(v => v.NoUtilisateur == noUtilisateur && v.NoPreference == NoPreferenceTaillePage)
                .Select(v => v.Valeur)
                .FirstOrDefaultAsync();

            return int.TryParse(valeur, out int n) && n >= 6 && n <= 99 ? n : TaillePageParDefaut;
        }



        // POUR VOIR LES DETAILS D'UN DVD
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var exemplaire = await _db.Exemplaires
                .Include(e => e.Proprietaire)
                .Include(e => e.Emprunts)
                .FirstOrDefaultAsync(e => e.NoExemplaire == id);

            if (exemplaire == null)
                return NotFound();

            int noFilm = exemplaire.NoExemplaire / FacteurExemplaire;

            var film = await _db.Films
                .Include(f => f.CategorieInfo)
                .Include(f => f.FormatInfo)
                .Include(f => f.Producteur)
                .Include(f => f.Realisateur)
                .Include(f => f.UtilisateurMAJ)
                .FirstOrDefaultAsync(f => f.NoFilm == noFilm);

            if (film == null)
                return NotFound();

            int noDetenteur = exemplaire.Emprunts
                .OrderByDescending(e => e.DateEmprunt)
                .Select(e => (int?)e.NoUtilisateur)
                .FirstOrDefault()
                ?? exemplaire.NoUtilisateurProprietaire;

            var detenteur = await _db.Utilisateurs
                .FirstOrDefaultAsync(u => u.Id == noDetenteur);

            ViewBag.Acteurs = await _db.FilmsActeurs
                .Where(x => x.NoFilm == noFilm)
                .Select(x => x.Acteur!.Nom)
                .OrderBy(n => n)
                .Take(3)
                .ToListAsync();

            ViewBag.Supplements = await _db.FilmsSupplements
                .Where(x => x.NoFilm == noFilm)
                .Select(x => x.Supplement!.Description)
                .OrderBy(n => n)
                .Take(3)
                .ToListAsync();

            ViewBag.Langues = await _db.FilmsLangues
                .Where(x => x.NoFilm == noFilm)
                .Select(x => x.Langue!.Nom)
                .OrderBy(n => n)
                .ToListAsync();

            ViewBag.SousTitres = await _db.FilmsSousTitres
                .Where(x => x.NoFilm == noFilm)
                .Select(x => x.SousTitre!.LangueSousTitre)
                .OrderBy(n => n)
                .ToListAsync();

            ViewBag.NoExemplaire = exemplaire.NoExemplaire;
            ViewBag.Proprietaire = exemplaire.Proprietaire;
            ViewBag.Detenteur = detenteur;

            return View(film);
        }



        // POUR VOIR LES DVD D'UN AUTRE UTIL
        [HttpGet]
        public async Task<IActionResult> AutreUtilisateur(int id, int page = 1)
        {
            int noUtilisateur = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var autre = await _db.Utilisateurs.FirstOrDefaultAsync(u => u.Id == id);
            if (autre == null)
                return NotFound();

            int taillePage = await ObtenirTaillePageAsync(noUtilisateur);

            var requete =
                from e in _db.Exemplaires
                join f in _db.Films on e.NoExemplaire / FacteurExemplaire equals f.NoFilm
                select new
                {
                    e.NoExemplaire,
                    f.Titre,
                    f.ImagePochette,
                    NoDetenteur = e.Emprunts
                        .OrderByDescending(x => x.DateEmprunt)
                        .Select(x => (int?)x.NoUtilisateur)
                        .FirstOrDefault() ?? e.NoUtilisateurProprietaire
                };

            // Seulement les DVDs que l'utilisateur choisi a en main
            var dvdsDeLautre = requete.Where(x => x.NoDetenteur == id);

            int total = await dvdsDeLautre.CountAsync();
            int nbPages = Math.Max(1, (int)Math.Ceiling(total / (double)taillePage));
            page = Math.Clamp(page, 1, nbPages);

            var films = await dvdsDeLautre
                .OrderBy(x => x.Titre)
                .ThenBy(x => x.NoExemplaire)
                .Skip((page - 1) * taillePage)
                .Take(taillePage)
                .Select(x => new FilmListeItem
                {
                    NoExemplaire = x.NoExemplaire,
                    Titre = x.Titre,
                    ImagePochette = x.ImagePochette,
                    NomEmprunteur = autre.UserName ?? "",
                    EstEnMain = x.NoDetenteur == noUtilisateur
                })
                .ToListAsync();

            for (int i = 0; i < films.Count; i++)
                films[i].Numero = (page - 1) * taillePage + i + 1;

            ViewBag.NoUtilisateurChoisi = id;
            ViewBag.NomUtilisateurChoisi = autre.UserName;
            ViewBag.Page = page;
            ViewBag.NbPages = nbPages;
            ViewBag.Total = total;
            ViewBag.Utilisateurs = await _db.Utilisateurs
                .Where(u => u.TypeUtilisateur != "A" && u.Id != noUtilisateur)
                .OrderBy(u => u.UserName)
                .ToListAsync();

            return View(films);
        }
        public IActionResult Modifier(int id) => View("AVenir");
        public IActionResult Supprimer(int id) => View("AVenir");
        public IActionResult Appropriation(int id) => View("AVenir");
        public IActionResult Courriel(int id) => View("AVenir");
    }
}
