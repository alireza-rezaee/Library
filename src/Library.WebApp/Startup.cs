using Mohkazv.Library.WebApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mohkazv.Library.WebApp.Areas.Identity.Data;
using Mohkazv.Library.WebApp.Areas.Identity.Helpers;
using Mohkazv.Library.WebApp.Services.Email;
using Mohkazv.Library.WebApp.Helpers;

namespace Mohkazv.Library.WebApp
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
            services.AddSession(options =>
            {
                options.Cookie.Name = ".LibraryWebApp.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            var microsoftAuth = Configuration.GetSection("Authentication").GetSection("Microsoft");
            var gitHubAuth = Configuration.GetSection("Authentication").GetSection("GitHub");
            var stackExchangeAuth = Configuration.GetSection("Authentication").GetSection("StackExchange");
            services.AddAuthentication()
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = microsoftAuth.GetSection("ClientId").Value;
                    microsoftOptions.ClientSecret = microsoftAuth.GetSection("ClientSecret").Value;
                    microsoftOptions.CallbackPath = "/signin-microsoft";
                })
                .AddGitHub(gitHubOptions =>
                {
                    gitHubOptions.ClientId = gitHubAuth.GetSection("ClientId").Value;
                    gitHubOptions.ClientSecret = gitHubAuth.GetSection("ClientSecret").Value;
                    gitHubOptions.CallbackPath = "/signin-github";
                })
                .AddStackExchange(stackExchangeOptions =>
                {
                    stackExchangeOptions.ClientId = stackExchangeAuth.GetSection("ClientId").Value;
                    stackExchangeOptions.ClientSecret = stackExchangeAuth.GetSection("ClientSecret").Value;
                    stackExchangeOptions.CallbackPath = "/signin-stackexchange";
                    stackExchangeOptions.RequestKey = stackExchangeAuth.GetSection("Key").Value;
                });

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<ISiteEmailSender, SiteEmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
