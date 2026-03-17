using InfoTrack.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Solicitor> Solicitors => Set<Solicitor>();
}