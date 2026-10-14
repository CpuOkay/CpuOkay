using System.ComponentModel.DataAnnotations;

namespace EinnahmenAusgabenRechner.Models
{
    public class Eintrag
    {
        public int Id { get; set; }
        public double Betrag { get; set; }
        public DateTime Datum { get; set; }
        
        [MaxLength (50) ]        
        public string Beschreibung { get; set; }
        public int KategorieId { get; set; }
        public int EintragTypId { get; set; }   
        
        public Kategorie? Kategorie { get; set; }
        public EintragTyp? EintragTyp { get; set; }

    }
}
