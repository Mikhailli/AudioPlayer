﻿using AudioPlayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AudioPlayer;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddControllersWithViews();
        //services.AddHttpClient<>();
    }
 
    public void Configure(IApplicationBuilder app)
    {
        app.UseDeveloperExceptionPage();
             
        app.UseStaticFiles();
        
        app.UseRouting();
 
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}