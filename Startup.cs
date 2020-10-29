using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyExpenseRui.Data;
using DailyExpenseRui.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DailyExpenseRui
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

            //services.AddScoped<IExpenseService, ExpenseService>();
            services.AddTransient(typeof(IExpenseService), typeof(ExpenseService));
            //var registerS = new RegisterServices();
            //RegisterServices.ConfigureServices(services);

            services.AddDbContext<ExpenseContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }



    static class RegisterServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IExpenseService, ExpenseService>();
        }
    }

}
