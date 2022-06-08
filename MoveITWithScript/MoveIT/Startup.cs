using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using MoveIT.Data;
using MoveIT.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System;
using MoveIT.Services;
using IdentityServer4.Services;
using System.Security.Claims;
using MoveITWeb.Services;
using MoveITWeb.Builders;
using AutoMapper;

namespace MoveIT
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

              services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)            
                .AddEntityFrameworkStores<ApplicationDbContext>();



            services.AddIdentityServer(o => o.Authentication.CookieLifetime = TimeSpan.FromHours(2))
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            

            services.AddTransient<IProfileService, ProfileService>();
            services.AddScoped<RelocationPriceService, RelocationPriceService>();
            services.AddScoped<RelocationOfferService, RelocationOfferService>();
            services.AddScoped<RelocationOfferReferenceService, RelocationOfferReferenceService>();
            services.AddScoped<RelocationCustomerOfferViewModelBuilder, RelocationCustomerOfferViewModelBuilder>();
            services.AddScoped<ApplicationUserInfoService, ApplicationUserInfoService>();


            



            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();
            services.AddRazorPages();

           // services.AddHttpContextAccessor();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdministrator",
                     policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
         
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });


            CreateDefaultAdministrator(services).Wait();
          
        }

        private async Task CreateDefaultAdministrator(IServiceProvider serviceProvider)
        {

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var adminEmail = Configuration["General:AdministratorEmail"];
            var adminPassword = Configuration["General:AdministratorPassword"];

            var user = await userManager.FindByNameAsync(adminEmail);
            if (user == null)
            {
                var result = await userManager.CreateAsync(new ApplicationUser(adminEmail) { Email = adminEmail, EmailConfirmed = true }, adminPassword);
                
                if(result.Succeeded)
                {
                    user = await userManager.FindByNameAsync(adminEmail);
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator"));
                }
                
                
            }
        }
    }
}
