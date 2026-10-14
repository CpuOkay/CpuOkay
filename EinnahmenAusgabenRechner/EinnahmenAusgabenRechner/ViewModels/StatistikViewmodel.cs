namespace EinnahmenAusgabenRechner.ViewModels
{
    public class StatistikViewmodel
    {
        public double DurchschnittEinnahmen {  get; set; }
        public double DurchschnittAusgaben { get; set; }
        public double Kontostand { get; set; }
        
        public int AnzahlEinnahmen { get; set; }
        public int AnzahlAusgaben { get; set; }
        public int AnzahlEinUndAusgaben { get; set; }

        public DateOnly? DatumVon {  get; set; }
        public DateOnly? DatumBis { get; set; }
        public int? KategorieId { get; set; }
    }
}
