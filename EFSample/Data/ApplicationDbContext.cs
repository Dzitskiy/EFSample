using EFSample.Models;
using Microsoft.EntityFrameworkCore;

namespace EFSample.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }




        //public DbSet<Manager> Managers { get; set; }
        //public DbSet<Employee> Employees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        
        {
            Database.EnsureDeleted();

            var isAvalable = Database.CanConnect();
            var result = isAvalable ? "Ok!" : "Fail";

            Console.WriteLine($"Try tot connect: {result}");

            bool isCreated = Database.EnsureCreated();
            if (isCreated)
            {
                Console.WriteLine("db created!");
            }

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=SQLiteTestDB.db");

            //optionsBuilder
            //    .LogTo(Console.WriteLine)
            //    .EnableDetailedErrors();

            //optionsBuilder.UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<User>().UseTphMappingStrategy();

            //modelBuilder.Entity<User>().UseTptMappingStrategy();

            //modelBuilder.Entity<User>().UseTpcMappingStrategy();

            //modelBuilder.Entity<Employee>().ToTable("Employees");
            //modelBuilder.Entity<Manager>().ToTable("Managers");

            modelBuilder.Entity<User>().HasData(new User { UserId =1, Name = "User" });
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile { UserProfileId = 1, UserId = 1, Address = "Adr" });
            modelBuilder.Entity<Order>().HasData(new Order { OrderId = 1, UserId = 1 });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 1, OrderId = 1, Name ="Product" });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasMany(u => u.Products)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity(pc => pc.ToTable("ProductCategories"));

        }
    }
}
