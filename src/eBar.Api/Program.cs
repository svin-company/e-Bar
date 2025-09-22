using System.Diagnostics.CodeAnalysis;
using eBar.Api.Controllers;
using eBar.Api.Interfaces;
using eBar.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eBar.Api;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddControllers();
        builder.Services.AddSingleton<ITest, Test>();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
                policy.WithOrigins("http://localhost:5000")
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod());
        });
        
        var app = builder.Build();
        
        app.UseRouting();
        app.UseCors();
        app.MapControllers();
        
        app.Run();
    }
}
