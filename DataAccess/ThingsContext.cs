using BE_LoansApp.Entities;
using BE_LoansApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BE_LoansApp.DataAccess
{
    public class ThingsContext: IdentityDbContext
    {
        public ThingsContext(DbContextOptions<ThingsContext> options) 
            : base(options)
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Thing> Things { get; set; }

        public DbSet<Loan> Loans { get; set; }        
    }
}
