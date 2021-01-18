using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ThanhTuan.IDP.Data;
using ThanhTuan.IDP.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using ThanhTuan.IDP.Hydra;

namespace ThanhTuan.IDP
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
    public IConfiguration Configuration { get; }
    public static readonly ILoggerFactory factory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy(name: "all", builder =>
              {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
              });
      });
      services.AddDbContext<ApplicationDbContext>(options =>
      {
        options.UseNpgsql(Configuration.GetConnectionString("DatabaseURL"));
      });

      services.AddDefaultIdentity<ApplicationUser>()
      .AddRoles<ApplicationRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddSignInManager();

      services.AddScoped<IHydra, Hydra.Hydra>();
      services.AddSingleton<AccessDecision.AccessDecision>();
      services.AddControllers();
      services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      using (var scope = app.ApplicationServices.CreateScope())
      {
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
        dbContext.Database.Migrate();
        var seeder = new DataSeeder(userManager, roleManager);
        seeder.Seed();

      }
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });
      app.UseRouting();
      app.UseCors("all");
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });
    }
  }
}
