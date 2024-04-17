
using APIConnectify.NET.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer;
using Npgsql.EntityFrameworkCore.PostgreSQL;
namespace APIConnectify.NET
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorizationPolicyEvaluator();
            builder.Services.AddCors();
            builder.Services.AddHttpClient();
            //builder.Services.AddDbContext<DB>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("APIConnectifyNETContext") ?? throw new InvalidOperationException("Connection string 'APIConnectifyNETContext' not found.")));

            builder.Services.AddDbContext<DB>(options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("APIConnectifyNETContext") ?? throw new InvalidOperationException("Connection string 'APIConnectifyNETContext' not found.")));
            //using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
            //{
            //    var migrationDbContext = serviceScope.ServiceProvider.GetRequiredService<DB>();

            //    if (migrationDbContext.Database.GetPendingMigrations().Any())
            //    {
            //        var migrator = migrationDbContext.GetService<IMigrator>();
            //        migrator.Migrate();
            //    }
            //}

            ////   builder.Services.AddDbContext<DB>(options =>
            //      options.UseNpgsql(builder.Configuration.GetConnectionString("APIConnectifyNETContext") ?? throw new InvalidOperationException("Connection string 'APIConnectifyNETContext' not found.")));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader().WithHeaders();
                    });
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseCors(builder => builder.AllowAnyMethod());

            app.UseHttpsRedirection();
            app.UseRouting();
      
            app.UseCors("AllowAll");


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
