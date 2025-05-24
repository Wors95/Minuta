using Microsoft.EntityFrameworkCore;
using SiteRelogios.Models;

namespace SiteRelogios.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Relogio> Relogios => Set<Relogio>();
}
