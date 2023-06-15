using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Data;
using IS_Proj_HIT.Entities.Data;
using IS_Proj_HIT.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

//todo added this to solve uiframework


namespace IS_Proj_HIT
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        static readonly string _RequireAuthenticatedUserPolicy = "RequireAuthenticatedUserPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:HIT:ConnectionString"]));
            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDbContext<WCTCHealthSystemContext>(options => options.UseSqlServer(Configuration["Data:HIT:ConnectionString"]));
            services.AddTransient<IWCTCHealthSystemRepository, EFWCTCHealthSystemRepository>();

            var builder = services.AddMvc();
            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }

            services.AddScoped<IPhysicianAssessmentService, PhysicianAssessmentService>();
            services.AddScoped<IBodySystemsService, BodySystemsService>();
            services.AddScoped<IEncounterService, EncounterService>();
            services.AddScoped<IPhysicianService, PhysicianService>();
            services.AddScoped<IAllergenService, AllergenService>();
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddAuthorization(o => o.AddPolicy(_RequireAuthenticatedUserPolicy, builder => builder.RequireAuthenticatedUser()));

            //services.AddPaging();

            //We may need 59-64 --------------------------------------------------------------------------------------
                // .AddRazorPagesOptions(options =>
                // {
                //     options.AllowAreas = true;
                //     options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                //     options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                // });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            // todo updated 86-92 --------------------------------------------------------------------------------------
            app.UseCors();
            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //todo added this for the update
                endpoints.MapDefaultControllerRoute().RequireAuthorization(_RequireAuthenticatedUserPolicy);

                endpoints.MapRazorPages();
            });

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //     name: "pagination",
            //         template: "Patients/Page{patientPage}",
            //         defaults: new { Controller = "Patient", action = "Index" });
            //     routes.MapRoute(
            //         name: "default",
            //         template: "{controller=Home}/{action=Index}/{id?}");
            // });
        }
    }
}
