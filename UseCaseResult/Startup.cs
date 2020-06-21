using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Orders.Api.Mappings;
using Orders.Application;
using Orders.Data;
using Orders.Domain.Interfaces;
using Orders.Infrastructure;

namespace Orders.Api
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
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddAutoMapper(
                typeof(IHasMappingProfile).Assembly,
                typeof(Infrastructure.Mappings.IHasMappingProfile).Assembly
            );

            services.AddSingleton<IOrdersRepository, OrdersRepository>(); // singleton так как InMemory
            services.AddSingleton<ICustomerRepository, CustomerClient>();
            services.AddSingleton<OrdersPresenter>();
            services.AddSingleton<AddOrderUseCase>();

            services.AddSwaggerGenNewtonsoftSupport()
                    .AddSwaggerGen(options =>
                    {
                        options.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Title = "Сервис заказов",
                            Version = "v1",
                        });
                        options.IgnoreObsoleteActions();
                        options.IgnoreObsoleteProperties();
                        options.DescribeAllParametersInCamelCase();
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "swagger";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Сервис заказов");
                });
        }
    }
}
