using Microsoft.EntityFrameworkCore;
using VerveClothingApi.Entities;

namespace VerveClothingApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionProduct> CollectionProducts { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //  many-to-many relationships
            modelBuilder.Entity<CategoryProduct>()
                .HasKey(cp => new { cp.CategoryId, cp.ProductId });

            modelBuilder.Entity<CollectionProduct>()
                .HasKey(cp => new { cp.CollectionId, cp.ProductId });

            modelBuilder.Entity<ProductTag>()
                .HasKey(pt => new { pt.ProductId, pt.TagId });

            //  one-to-one relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingAddress)
                .WithOne(sa => sa.Order)
                .HasForeignKey<ShippingAddress>(sa => sa.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.PaymentTransaction)
                .WithOne(pt => pt.Order)
                .HasForeignKey<PaymentTransaction>(pt => pt.OrderId);

            // one-to-many relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User);
            modelBuilder.Entity<User>()
                .HasMany(u => u.WishlistItems)
                .WithOne(wi => wi.User);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders) .WithOne(o => o.User);
        }
    }
}
