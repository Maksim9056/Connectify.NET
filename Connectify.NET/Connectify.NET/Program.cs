using Connectify.NET.Client.Pages;
using Connectify.NET.Components;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel;

namespace Connectify.NET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddHttpClient();
            builder.Services.AddCors();
            builder.Services.AddScoped<Connectify.NET.Components.Pages.Component>();
            builder.Services.AddScoped<Connectify.NET.Components.Pages.GroupAdd>();
            builder.Services.AddScoped<Connectify.NET.Components.Pages.GroupChats>();
            builder.Services.AddScoped<Connectify.NET.Components.Pages.ListChatsAll>();
            builder.Services.AddScoped<Connectify.NET.Components.Pages.Friends>();
            builder.Services.AddScoped<Component>();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            var port = 5200;
        

            app.Run();
        }
    }
}
