#pragma warning disable 1591
using API.WithAuthentication.Extensions;
using API.WithAuthentication.Models;
using API.WithAuthentication.Services;
using JwtAuthentication.Shared;
using JwtAuthentication.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace API.WithAuthentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            IConfigurationSection settingsSection = Configuration.GetSection("AppSettings");
            AppSettings settings = settingsSection.Get<AppSettings>();
            byte[] signingKey = Encoding.UTF8.GetBytes(settings.EncryptionKey);
            services.AddAuthentication(signingKey);

            services.AddSwaggerDocumentation();

            services.Configure<AppSettings>(settingsSection);
            services.AddTransient<UserRepository>();
            services.AddTransient<UserService>();
            services.AddTransient<AuthenticationService>();
            services.AddTransient<TokenService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerDocumentation();
        }
    }
}
