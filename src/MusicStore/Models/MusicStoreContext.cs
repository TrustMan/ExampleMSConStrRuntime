using System;//add line
using System.ComponentModel.DataAnnotations.Schema; //add line
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MusicStore.Models
{
    public class ApplicationUser : IdentityUser //Override filds NormalizedName ,Name as under
    {
        [Column(TypeName = "varchar(255)")]
        public override string Email { get; set; }

        [Column(TypeName = "varchar(255)")]
        public override string UserName { get; set; }

        [Column(TypeName = "varchar(255)")]
        public override string NormalizedEmail { get; set; }

        [Column(TypeName = "varchar(255)")]
        public override string NormalizedUserName { get; set; }

        [Column(TypeName = "datetime")] //add lines
        public override DateTimeOffset? LockoutEnd { get; set; }
    }

    public class ApplicationRole : IdentityRole //Add class ApplicationRole and override filds NormalizedName ,Name 
    {
        [Column(TypeName = "varchar(255)")]
        public override string NormalizedName { get; set; }

        [Column(TypeName = "varchar(255)")]
        public override string Name { get; set; }
    }
    public class MusicStoreContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public MusicStoreContext(DbContextOptions<MusicStoreContext> options)
            : base(options)
        {
            ProviderSpecified = options.Extensions.Any(); //Add line
            // TODO: #639
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public bool ProviderSpecified { get; private set; } //Add property
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}