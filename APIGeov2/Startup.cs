using API.Data;
using API.Data.EF;
using API.Eventos;
using API.Servicios;
using EventBusv2;
using EventBusv2.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQEventBus;

namespace APIGeov2
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
            services.AddSwaggerGen();
            services.AddScoped(typeof(ServicioDeDireccionesAGeolocalizar));
            services.AddSingleton<IGestorDeSuscripciones, GestorDeSuscripcionesEnMemoria>();
            services.AddScoped<IDireccionRepositorio, DireccionRepositorioEF>();
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var exchange = Configuration["ExchangeName"];
                var qNombre = Configuration["QueueName"];
                var gestorSuscripciones = sp.GetRequiredService<IGestorDeSuscripciones>();
                return new EventBusRabbitMQ(new ConnectionFactory() { HostName = "localhost" }, gestorSuscripciones, exchange, qNombre);
            });
            services.AddDbContext<DireccionDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DireccionDbContext")));

            ConfigureEventBus(services);
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Geo V1");
                c.RoutePrefix = string.Empty;
            });
        }


        protected virtual void ConfigureEventBus(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var eventBus = serviceProvider.GetService<IEventBus>();
            var servicioDirecciones = serviceProvider.GetService<ServicioDeDireccionesAGeolocalizar>();
            eventBus.Suscribir<EventoDeIntegracionGeolocalizacionRealizada, EventoDeIntegracionGeolocalizacionRealizadaHandler>
                (new EventoDeIntegracionGeolocalizacionRealizadaHandler(servicioDirecciones));
        }
    }
}
