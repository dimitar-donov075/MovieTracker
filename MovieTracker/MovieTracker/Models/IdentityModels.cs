using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
namespace MovieTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Anime> Animes { get; set; }
        public virtual ICollection<Series> TvSeries { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> TvSeries { get; set; }
    }
}