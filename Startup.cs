using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Task4.Models;

public class Startup
{
    public IConfiguration configRoot
    {
        get;
    }
    public Startup(IConfiguration configuration)
    {
        configRoot = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        var connectionString = Configuration.GetConnectionString("MongoDB");
        var client = new MongoClient(connectionString);

        services.AddSingleton(x => new ImageDataContext(client));
        services.AddTransient<FileUploadService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Upload}/{action=Index}/{id?}");
        });
    }

    }
