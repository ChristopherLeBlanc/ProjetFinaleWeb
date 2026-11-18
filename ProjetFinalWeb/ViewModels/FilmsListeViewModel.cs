namespace ProjetFinalWeb.ViewModels
{
    public class FilmListeItem
    {
        public int Numero { get; set; }
        public int NoExemplaire { get; set; }
        public string TitreFrancais { get; set; } = "";
        public string? ImagePochette { get; set; }
        public string NomEmprunteur { get; set; } = "";
        public bool EstEnMain { get; set; }
    }

    public class FilmsListeViewModel
    {
        public List<FilmListeItem> Films { get; set; } = new();
        public int Page { get; set; }
        public int NbPages { get; set; }
        public int TaillePage { get; set; }
        public int Total { get; set; }
        public string Tri { get; set; } = "titre";
        public string? Recherche { get; set; }
    }
}
