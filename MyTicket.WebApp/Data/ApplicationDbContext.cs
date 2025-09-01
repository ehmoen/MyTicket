using Microsoft.EntityFrameworkCore;
using MyTicket.WebApp.Data.Entities;

namespace MyTicket.WebApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSets for your entities
    public DbSet<Event>? Events { get; set; }
    
    
}