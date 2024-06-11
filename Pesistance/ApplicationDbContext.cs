using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Product>()
        //        .Property(p => p.Price)
        //        .HasColumnType("decimal(18, 0)");
        //    builder.Entity<Combo>()
        //        .Property(c => c.Price)
        //        .HasColumnType("decimal(18, 0)");
        //    builder.Entity<Cart>()
        //        .Property(p => p.Price)
        //        .HasColumnType("decimal(18, 0)");

        //    builder.Entity<IdentityUserLogin<string>>(entity =>
        //    {
        //        entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        //    });

        //    builder.Entity<IdentityUserRole<string>>(entity =>
        //    {
        //        entity.HasKey(e => new { e.UserId, e.RoleId });
        //    });

        //    builder.Entity<IdentityUserToken<string>>(entity =>
        //    {
        //        entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        //    });
        //    base.OnModelCreating(builder);

        //}
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCombo> ProductCombos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
