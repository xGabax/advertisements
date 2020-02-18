using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using DrendencyDemo.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using DrendencyDemo.Web.Infrastructure.Swagger;
using DrendencyDemo.Web.Infrastructure.Middlewares;
using TrendencyDemo.Dal.Entities;
using DrendencyDemo.Web.Infrastructure.Authentication;
using Hangfire;
using TrendencyDemo.Web.Infrastructure.Jobs;
using TrendencyDemo.CommonModule.Interfaces;
using TrendencyDemo.CommonModule.Services;
using TrendencyDemo.Common.Configs;
using TrendencyDemo.CommonModule.Aggregates;

namespace DrendencyDemo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmailConfigs>(Configuration.GetSection("EmailConfigs"));
            services.Configure<EmailCredentials>(Configuration.GetSection("EmailCredentials"));

            services.AddDbContext<TrendencyDemoDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<TrendencyDemoDbContext>();

            services.ConfigureAuthenticationServices();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.ConfigureSwaggerServices();

            services.AddHangfire(config =>
                config.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddTransient<EmailSenderJob>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ServiceBaseAggregate>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider, EmailSenderJob emailSenderJob)
        {
            app.AddExceptionHandler();
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.ConfigureSwagger(provider);
            
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate(() => emailSenderJob.SendEmails(), Cron.Minutely);

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
