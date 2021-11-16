using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.DAL;
using Webshop.DataSource;
using Webshop.DTO;

namespace Webshop.UI
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
            services.AddSingleton<IDataSource<CardDTO>, Source_Card>()
                .AddSingleton<IDataSource<CartDTO>, Source_Cart>()
                .AddSingleton<IDataSource<CustomerDTO>, Source_Customer>()
                .AddSingleton<IDataSource<OrderDTO>, Source_Order>()
                .AddSingleton<IDataSource<ProductDTO>, Source_Product>()
                .AddSingleton<IDataSource<ReceiptDTO>, Source_Receipt>()
                .AddSingleton<DAL_Customer>()
                .AddSingleton<DAL_Card>()
                .AddSingleton<DAL_Cart>()
                .AddSingleton<DAL_Order>()
                .AddSingleton<DAL_Product>()
                .AddSingleton<DAL_Receipt>()
                .AddSession(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                })
                .AddDistributedMemoryCache()
                .AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
