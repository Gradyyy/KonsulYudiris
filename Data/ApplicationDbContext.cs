using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KonsulYudiris.Models;

namespace KonsulYudiris.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<KonsulYudiris.Models.Case>? Case { get; set; }
        public DbSet<KonsulYudiris.Models.Consultant>? Consultant { get; set; }
    }
}