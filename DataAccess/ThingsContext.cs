using BE_LoansApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BE_LoansApp.DataAccess
{
    public class ThingsContext: DbContext
    {
        public ThingsContext(DbContextOptions<ThingsContext> options) 
            : base(options)
        { 
        
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Thing> Things { get; set; }

        public DbSet<Loan> Loans { get; set; }        
    }
}
