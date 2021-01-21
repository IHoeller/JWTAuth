using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;


namespace JWTAuth.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped<AuthenticationStateProvider, Helpers.AuthStateProvider>();
            builder.Services.AddScoped<Services.IAuthService, Services.AuthService>();


            var host = builder.Build();
               
            await host.RunAsync();
        }
    }
}
