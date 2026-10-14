namespace EinnahmenAusgabenRechner.Models
{
    public class Kategorie
    {
        public int Id { get; set; }
        public string Bezeichnung { get; set; }
        public string? Farbe {  get; set; }

        public List<Eintrag> Eintrag { get; set; } = new List<Eintrag>();
    }
}
