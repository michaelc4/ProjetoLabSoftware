﻿using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped(typeof(IIdeiaAnexoRepository), typeof(IdeiaAnexoImplementation));
            serviceCollection.AddScoped(typeof(IIdeiaRepository), typeof(IdeiaImplementation));
            serviceCollection.AddScoped(typeof(ILikeRepository), typeof(LikeImplementation));
            serviceCollection.AddScoped(typeof(IProblemaAnexoRepository), typeof(ProblemaAnexoImplementation));
            serviceCollection.AddScoped(typeof(IProblemaRepository), typeof(ProblemaImplementation));
            serviceCollection.AddScoped(typeof(IUsuarioRepository), typeof(UsuarioImplementation));
            serviceCollection.AddScoped(typeof(IVoluntarioRepository), typeof(VoluntarioImplementation));

            var connectionString = "server=dbapiinova.mysql.database.azure.com;port=3306;database=dbapiinova;uid=dbapiinova@dbapiinova;password=root@inova123";
            serviceCollection.AddDbContext<MyContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)), mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)));
        }
    }
}
