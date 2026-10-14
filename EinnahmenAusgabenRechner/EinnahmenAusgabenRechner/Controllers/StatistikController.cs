using EinnahmenAusgabenRechner.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScottPlot;

namespace EinnahmenAusgabenRechner.Controllers
{
    public class StatistikController : Controller
    {
        private readonly EinnahmenAusgabenRechnerContext _context;

        public StatistikController(EinnahmenAusgabenRechnerContext context)
        {
            _context = context;
        }
        public IActionResult Index(StatistikViewmodel viewmodel)
        {
            SetViewData();

            var einnahmenQuery = _context.Eintrag.Where(z => z.EintragTypId == 1).AsQueryable();
            var ausgabenQuery = _context.Eintrag.Where(z => z.EintragTypId == 2).AsQueryable();

            if (viewmodel.KategorieId.HasValue)
            {
                einnahmenQuery = einnahmenQuery.Where(z => z.KategorieId == viewmodel.KategorieId.Value);
                ausgabenQuery = ausgabenQuery.Where(z => z.KategorieId == viewmodel.KategorieId.Value);
            }

            if (viewmodel.DatumVon.HasValue)
            {
                einnahmenQuery = einnahmenQuery.Where(z => DateOnly.FromDateTime(z.Datum) >= viewmodel.DatumVon.Value);
                ausgabenQuery = ausgabenQuery.Where(z => DateOnly.FromDateTime(z.Datum) >= viewmodel.DatumVon.Value);
            }

            if (viewmodel.DatumBis.HasValue)
            {
                einnahmenQuery = einnahmenQuery.Where(z => DateOnly.FromDateTime(z.Datum) <= viewmodel.DatumBis.Value);
                ausgabenQuery = ausgabenQuery.Where(z => DateOnly.FromDateTime(z.Datum) <= viewmodel.DatumBis.Value);
            }


            double avgeinnahmen = einnahmenQuery.Any() ? einnahmenQuery.Average(z => z.Betrag) : 0;
            double avgAusgaben = ausgabenQuery.Any() ? ausgabenQuery.Average(z => z.Betrag) : 0;


            int anzahlEinnahmen = einnahmenQuery.Count();
            int anzahlAusgaben = ausgabenQuery.Count();
            int anzahlEinUndAusgaben = anzahlEinnahmen + anzahlAusgaben;

            viewmodel.DurchschnittEinnahmen = avgeinnahmen;
            viewmodel.DurchschnittAusgaben = avgAusgaben;

            viewmodel.AnzahlEinnahmen = anzahlEinnahmen;
            viewmodel.AnzahlAusgaben = anzahlAusgaben;
            viewmodel.AnzahlEinUndAusgaben = anzahlEinUndAusgaben;


            return View(viewmodel);
        }

        public IActionResult FuenfHoechsteAusgaben(StatistikViewmodel viewmodel)
        {
            var ausgabenQuery = _context.Eintrag.Include(x=> x.Kategorie)
                .Where(z => z.EintragTypId == 2).AsQueryable();

            if (viewmodel.KategorieId.HasValue)
            {
                ausgabenQuery = ausgabenQuery.Where(z => z.KategorieId == viewmodel.KategorieId.Value);

            }

            if (viewmodel.DatumVon.HasValue)
            {
                ausgabenQuery = ausgabenQuery.Where(z => DateOnly.FromDateTime(z.Datum) >= viewmodel.DatumVon.Value);
            }

            if (viewmodel.DatumBis.HasValue)
            {
                ausgabenQuery = ausgabenQuery.Where(z => DateOnly.FromDateTime(z.Datum) <= viewmodel.DatumBis.Value);
            }

            var fuenfHoechsteAusgabenData = ausgabenQuery
                .OrderByDescending(z => z.Betrag)
                .Take(5)
                .ToList();

            double[] values = fuenfHoechsteAusgabenData
                .Select(z => (double)z.Betrag)
                .ToArray();

            string[] namen = fuenfHoechsteAusgabenData
                .Select(z => z.Kategorie.Bezeichnung)
                .ToArray();

            var plot = new Plot();
            var bars = plot.Add.Bars(values);
            bars.Color = Colors.Orange;

            bars.ValueLabelStyle.IsVisible = true;

            double[] positions = Enumerable.Range(0, values.Length)
                .Select(i => (double)i)
                .ToArray();

            plot.Axes.Bottom.TickGenerator =
                new ScottPlot.TickGenerators.NumericManual(positions, namen);


            plot.Title("Die fünf höchsten Ausgaben");
            plot.XLabel("Kategorie");
            plot.YLabel("Betrag (€)");

            return File(plot.GetImageBytes(900, 900), "image/png");
        }

        public IActionResult FuenfHoechsteinnahmen(StatistikViewmodel viewmodel)
        {

            var einnahmeQuery = _context.Eintrag.Include(x => x.Kategorie)
               .Include(z => z.Kategorie)
               .Where(z => z.EintragTypId == 1).AsQueryable();

            if (viewmodel.KategorieId.HasValue)
            {
                einnahmeQuery = einnahmeQuery.Where(z => z.KategorieId == viewmodel.KategorieId.Value);

            }

            if (viewmodel.DatumVon.HasValue)
            {
                einnahmeQuery = einnahmeQuery.Where(z => DateOnly.FromDateTime(z.Datum) >= viewmodel.DatumVon.Value);
            }

            if (viewmodel.DatumBis.HasValue)
            {
                einnahmeQuery = einnahmeQuery.Where(z => DateOnly.FromDateTime(z.Datum) <= viewmodel.DatumBis.Value);
            }

            var fuenfHoechsteAusgabenData = einnahmeQuery
                .OrderByDescending(z => z.Betrag)
                .Take(5)
                .ToList();

            // Values mit Beträgen
            double[] values = fuenfHoechsteAusgabenData
                .Select(z => (double)z.Betrag)
                .ToArray();

            string[] namen = fuenfHoechsteAusgabenData
                .Select(z => z.Kategorie.Bezeichnung)
                .ToArray();

            var plot = new Plot();
            var bars = plot.Add.Bars(values);
            bars.Color = Colors.Green;

            bars.ValueLabelStyle.IsVisible = true;

            double[] positions = Enumerable.Range(0, values.Length)
                .Select(i => (double)i)
                .ToArray();

            plot.Axes.Bottom.TickGenerator =
                new ScottPlot.TickGenerators.NumericManual(positions, namen);


            plot.Title("Die fünf höchsten Einnahmen");
            plot.XLabel("Kategorie");
            plot.YLabel("Betrag (€)");



            return File(plot.GetImageBytes(900, 900), "image/png");
        }


        private void SetViewData()
        {
            var kategorien = _context.Kategorie.ToList();
            ViewData["Kategorien"] = kategorien;
        }
    }
}
