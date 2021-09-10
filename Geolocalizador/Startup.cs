using EventBusv2;
using EventBusv2.Base;
using Geocodificador.Eventos;
using Geocodificador.Modelos;
using Geocodificador.Servicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQEventBus;

namespace Geolocalizador
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
            services.AddSingleton<IGestorDeSuscripciones, GestorDeSuscripcionesEnMemoria>();
            services.AddSingleton<IServicioDeGeocodificacion, ServicioDeGeocodificacionOSM>();
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var exchange = Configuration["ExchangeName"];
                var qNombre = Configuration["QueueName"];
                var gestorSuscripciones = sp.GetRequiredService<IGestorDeSuscripciones>();
                return new EventBusRabbitMQ(new ConnectionFactory() { HostName = "localhost" }, gestorSuscripciones, exchange, qNombre);
            });
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

        }

        protected virtual void ConfigureEventBus(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var eventBus = serviceProvider.GetService<IEventBus>();
            var geocodificador = serviceProvider.GetService<ServicioDeGeocodificacionOSM>();
            eventBus.Suscribir<EventoDeIntegracionDireccionAGeolocalizarCreada, EventoDeIntegracionDireccionAGeolocalizarCreadaHandler>
                (new EventoDeIntegracionDireccionAGeolocalizarCreadaHandler(geocodificador, eventBus));
        }
    }
}
