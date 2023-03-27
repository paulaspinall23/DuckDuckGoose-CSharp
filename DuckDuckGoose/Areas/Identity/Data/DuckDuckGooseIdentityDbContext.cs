using DuckDuckGoose.Areas.Identity.Data;
using DuckDuckGoose.Models.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DuckDuckGoose.Areas.Identity.Data;

public class DuckDuckGooseIdentityDbContext : IdentityDbContext<DuckDuckGooseUser>
{
    public DbSet<Honk> Honks { get; set; }
    
    public DuckDuckGooseIdentityDbContext(DbContextOptions<DuckDuckGooseIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
