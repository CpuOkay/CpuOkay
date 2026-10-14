using EinnahmenAusgabenRechner.Models;
using Microsoft.EntityFrameworkCore;

public class EinnahmenAusgabenRechnerContext(DbContextOptions<EinnahmenAusgabenRechnerContext> options) : DbContext(options)
{
    public DbSet<Eintrag> Eintrag { get; set; } = default!;
    public DbSet<EintragTyp> EintragTyp  { get; set; } = default!;
    public DbSet<Kategorie> Kategorie { get; set; } = default!;

}
