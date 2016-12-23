using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
       
    }
    public class ApplicationRole : IdentityRole //Add class ApplicationRole and override filds NormalizedName ,Name 
    {
        [Column(TypeName = "varchar(255)")]
        public override string NormalizedName { get; set; }

        [Column(TypeName = "varchar(255)")]
        public override string Name { get; set; }
    }

    


	public class MusicStoreContext : IdentityDbContext<ApplicationUser> //Delete line
    public class MusicStoreContext : IdentityDbContext<ApplicationUser, ApplicationRole, string> //Add line
    {
        
        public MusicStoreContext(DbContextOptions<MusicStoreContext> options)
            : base(options)
        {
            // TODO: #639
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}