using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UnitOfWork;
using Domain.Interfaces;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
         public static void ConfigureCors(this IServiceCollection services)=>
        services.AddCors(options=>
        {
            options.AddPolicy("CorsPolicy",builder=>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );
        }

        );
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // services.AddScoped<IPasswordHasher<Usuario>,PasswordHasher<Usuario>>();
            // services.AddScoped<IUserService, UserService>();
        }
    }
}