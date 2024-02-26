
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Sudoku.BL.Services;
using Sudoku.BL.Workflow.User;
using Sudoku.Core.Caching;
using Sudoku.Core.RabbitMq;
using Sudoku.DataAccess;
using Sudoku.Domain.ConfigureOptions;
using Sudoku.SudokuProcessor.Interfaces;
using Sudoku.SudokuProcessor.Options;
using Sudoku.SudokuProcessor.Services;
using System.Reflection;

namespace Sudoku.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.Configure<QueueOptions>(builder.Configuration.GetSection("RPC"));
        builder.Services.Configure<RpcOptions>(builder.Configuration.GetSection("RPC").GetSection("Rabbit"));
        builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection("Redis").GetSection("CacheKeys"));

        builder.Services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetSection("Redis")["ConnectionString"];
        });

        builder.Services.AddTransient<ISudokuBoardsService, SudokuBoardsService>();
        builder.Services.AddTransient<ICachedSudokuBoardService, CachedSudokuBoardService>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.SetUpDatabase(builder.Configuration);

        builder.Services.AddScoped<IRabbitService, RabbitService>();
        builder.Services.AddScoped<ICacheProvider, CacheProvider>();

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        builder.Services.AddMediatR(typeof(GetSudokuUserRequest).GetTypeInfo().Assembly);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
