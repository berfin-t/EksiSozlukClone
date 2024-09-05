using EksiSozlukClone.Api.Application.Interfaces.Repositories;
using EksiSozlukClone.Infastructure.Persistence.Context;
using EksiSozlukClone.Infastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EksiSozlukClone.Infastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DbContext>(conf =>
        {
            var connStr = configuration["EksiSozlukCloneDbConnectionString"].ToString();
            conf.UseNpgsql(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });

        //var seedData = new SeedData();
        //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEntryRepository, EntryRepository>();
        services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
        services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();

        return services;
    }
}
