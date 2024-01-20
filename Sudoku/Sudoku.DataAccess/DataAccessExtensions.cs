using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.DataAccess;

public static class DataAccessExtensions
{
    public static IServiceCollection SetUpDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionString"];
        var timeout = (int)TimeSpan.FromSeconds(60).TotalSeconds;

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, opts => opts.CommandTimeout(timeout));
            options.EnableSensitiveDataLogging();
        });

        return services;
    }
}
