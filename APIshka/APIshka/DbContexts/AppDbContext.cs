using Microsoft.EntityFrameworkCore;
using APIshka.Entities;


namespace APIshka.DbContexts
{


    public class AppDbContext : DbContext
    {

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<New> News { get; set; } = null!;


        public AppDbContext() // Конструктор без параметров
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;username=root;password=123456789;database=apishechka2",
                new MySqlServerVersion(new Version(8, 0, 40)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.UserId);

               
            });

            modelBuilder.Entity<New>(newModel =>
            {
                newModel.HasKey(n => n.NewsId);

                newModel
                .HasIndex(n => n.ImageName)
                .IsUnique();

            });

        }

    }
}
