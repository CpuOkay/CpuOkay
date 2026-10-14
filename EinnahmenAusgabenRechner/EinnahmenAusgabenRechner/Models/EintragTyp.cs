namespace EinnahmenAusgabenRechner.Models
{
    public class EintragTyp
    {
        public int Id { get; set; }
        public string Bezeichnung { get; set; }

        public List<Eintrag> Eintrag { get; set; } = new List<Eintrag>();
    }
}
