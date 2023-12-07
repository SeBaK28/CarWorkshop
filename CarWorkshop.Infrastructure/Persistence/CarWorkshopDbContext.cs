using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistence
{
    public class CarWorkshopDbContext :IdentityDbContext //po dodaniu Paczki Identity. Z DbContext zmieniłem na IdentityDbContext
    {
        public CarWorkshopDbContext(DbContextOptions<CarWorkshopDbContext> options) : base(options) 
        {

        }
        public DbSet<Domain.Entities.CarWorkshop> CarWorkshops { get; set; }

        public DbSet<Domain.Entities.CarWorkshopService> Services { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarWorkshopDb;Trusted_Connection=True;");  !!!nie używa się tego!!!
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//jest  po do aby klasa IdentityDbContext była w stanie również skonfigurować swoje tabele w których są inforamcje o userach
            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .OwnsOne(c => c.ContactDetails);

            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .HasMany(c=>  c.Services)
                .WithOne(s=> s.CarWorkshop)
                .HasForeignKey(s=>s.CarWorkshopId);
        }
    }
}
