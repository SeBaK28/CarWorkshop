using CarWorkshop.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarWorkshop.Infrastructure.Seeders;
using CarWorkshop.Infrastructure.Repositories;
using CarWorkshop.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CarWorkshop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarWorkshopDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("CarWorkshop")));
            services.AddScoped<CarWorkshopSeeder>();

            services.AddScoped<ICarWorkshopRepository, CarWorkshopRepository>();
            services.AddDefaultIdentity<IdentityUser>()//jako parametr generyczny przekazujemy klase usera aplikacji
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CarWorkshopDbContext>();//dzięki takiej konfiguracji cały mechanizm W Identity będzie w stanie integrować się z bazą danych aby w odpowiednie tabele i kolumny dodawać informacje o userach, ich rolach, czy loginach  
                
        }
    }
}
