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
                    f.TitreFrancais,
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
                    x.TitreFrancais,
                    x.TitreOriginal,
                    x.ImagePochette,
                    x.NoDetenteur,
                    NomDetenteur = u.UserName ?? ""
                };

            if (!string.IsNullOrWhiteSpace(recherche))
            {
                string r = recherche.Trim();
                requete = requete.Where(x => x.TitreFrancais.Contains(r)
                                          || (x.TitreOriginal != null && x.TitreOriginal.Contains(r)));
            }

            requete = tri switch
            {
                "nom"       => requete.OrderBy(x => x.NomDetenteur).ThenBy(x => x.NoExemplaire),
                "nom-titre" => requete.OrderBy(x => x.NomDetenteur).ThenBy(x => x.TitreFrancais),
                _           => requete.OrderBy(x => x.TitreFrancais).ThenBy(x => x.NoExemplaire)
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
                    TitreFrancais = x.TitreFrancais,
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

        public IActionResult Details(int id) => View("AVenir");
        public IActionResult Modifier(int id) => View("AVenir");
        public IActionResult Supprimer(int id) => View("AVenir");
        public IActionResult Appropriation(int id) => View("AVenir");
        public IActionResult Courriel(int id) => View("AVenir");
    }
}
