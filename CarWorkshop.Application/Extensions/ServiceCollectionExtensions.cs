using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            services.AddMediatR(typeof(CreateCarWorkshopCommand));
            //services.AddScoped<ICarWorkshopService, CarWorkshopService>();

            //services.AddMediatR(typeof(GetCarWorkshopByEncodedNameToEdit));

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new CarWorkshopMappingProfile(userContext));
            }).CreateMapper()
            );
            //services.AddAutoMapper(typeof(CarWorkshopMappingProfile));

            services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>() // rejestruje wszystkie walidatory, wystarczy raz podać param generyczny i przypisuje się do wszystkich
                .AddFluentValidationAutoValidation()    //walidacja z asp.net core jest zastąpiona paczką fluent Validation
                .AddFluentValidationClientsideAdapters();   //Po stronie front-end zostanie dodana odpowiednia logika która zwórci uwagę na wszystkie reguły walidacji jakie na nie nakładamy 
        }
    }
}
