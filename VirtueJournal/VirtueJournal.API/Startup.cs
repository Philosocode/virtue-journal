using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VirtueJournal.API.Middlewares;
using VirtueJournal.Data;
using VirtueJournal.Data.Repositories;
using VirtueJournal.Shared;

namespace VirtueJournal.API
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
            services.AddCors();
            services.AddDbContextPool<DataContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly("VirtueJournal.Data"))
            );
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc(setupAction => { setupAction.ReturnHttpNotAcceptable = true; }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = unitOfWork.Auth.GetByIdAsync(userId);
                        
                        // return unauthorized if user no longer exists
                        if (user == null)
                            context.Fail("Unauthorized");
                        
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            
            // configure DI for application services
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // In production, the React files will be served from this directory
//            services.AddSpaStaticFiles(configuration =>
//            {
//                configuration.RootPath = "ClientApp/build";
//            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
//            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
//            app.UseSpaStaticFiles();
            
            app.UseAuthentication();

            app.UseMvc();
            
//            app.UseSpa(spa =>
//            {
//                spa.Options.SourcePath = "../VirtueClient";
//
//                if (env.IsDevelopment())
//                {
//                    spa.UseReactDevelopmentServer(npmScript: "start");
//                }
//            });
        }
    }
}