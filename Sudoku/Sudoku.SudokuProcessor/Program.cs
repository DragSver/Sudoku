using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Sudoku.Core.Caching;
using Sudoku.Core.RabbitMq;
using Sudoku.Domain.ConfigureOptions;
using Sudoku.SudokuProcessor.Interfaces;
using Sudoku.SudokuProcessor.Options;
using Sudoku.SudokuProcessor.Services;

namespace Sudoku.SudokuProcessor;

public class Program
{
    public static void Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServices)
            .Build();
       
        host.Run();
    }

    private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
    {
        services.Configure<RpcOptions>(hostContext.Configuration.GetSection("RpcServer"));
        services.Configure<CacheOptions>(hostContext.Configuration.GetSection("Redis").GetSection("CacheKeys"));

        services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = hostContext.Configuration.GetSection("Redis")["ConnectionString"];
        });

        services.AddScoped<ICacheProvider, CacheProvider>();
        services.AddScoped<IRabbitService, RabbitService>();

        services.AddTransient<ISudokuBoardService, SudokuBoardService>();
        services.AddTransient<ICreateBoardService, CreateBoardService>();
        services.AddTransient<ICachedSudokuBoardService, CachedSudokuBoardService>();

        services.AddHostedService<Worker>();
    }
}