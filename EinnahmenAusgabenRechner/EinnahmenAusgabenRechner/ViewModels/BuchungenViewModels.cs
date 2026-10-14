using EinnahmenAusgabenRechner.Models;

namespace EinnahmenAusgabenRechner.ViewModels
{
    public class EintragenViewModel
    {
        public IEnumerable<Eintrag> Eintragen { get; set; } = new List<Eintrag>();
        public string? SearchBeschreibung { get; set; }
        public DateTime? SearchDatumVon { get; set; }
        public DateTime? SearchDatumBis { get; set; }
        public int? SearchEintragTyp { get; set; }
        public int? SearchKategorie {  get; set; }
        public int? AnzahlEintraege {  get; set; }

    }
}
