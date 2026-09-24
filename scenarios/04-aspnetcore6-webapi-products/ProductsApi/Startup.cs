using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;

namespace ProductsApi
{
    /// <summary>
    /// Classic ASP.NET Core Startup.cs / ConfigureServices+Configure split.
    /// Still fully supported on .NET 6, but most real .NET 6 codebases that
    /// were upgraded from .NET 5 (rather than created fresh) still look
    /// like this instead of adopting the newer top-level-statement
    /// "minimal hosting" style. Migrating to .NET 10 is a natural point to
    /// collapse this into a single top-level Program.cs.
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ProductsDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("ProductsDb")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
            context.Database.EnsureCreated();
            ProductsDbContext.SeedIfEmpty(context);
        }
    }
}
