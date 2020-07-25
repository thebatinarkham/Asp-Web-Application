using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
        
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<WebApplication.ViewModels.CartViewModel> CartViewModels { get; set; }

        //public System.Data.Entity.DbSet<WebApplication.ViewModels.CartViewModel> CartViewModels { get; set; }

        //public System.Data.Entity.DbSet<WebApplication.ViewModels.ProductViewModel> ProductViewModels { get; set; }
    }
}