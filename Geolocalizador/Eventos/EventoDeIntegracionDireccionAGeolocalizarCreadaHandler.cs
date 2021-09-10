using EventBusv2.Base;
using Geocodificador.Modelos;
using Geocodificador.Servicios;

namespace Geocodificador.Eventos
{
    public class EventoDeIntegracionDireccionAGeolocalizarCreadaHandler
        : IEventoDeIntegracionHandler<EventoDeIntegracionDireccionAGeolocalizarCreada>
    {
        private readonly IServicioDeGeocodificacion _servicioGeocodificacion;
        private readonly IEventBus _eventBus;

        public EventoDeIntegracionDireccionAGeolocalizarCreadaHandler(
            IServicioDeGeocodificacion servicioGeocodificacion,
            IEventBus eventBus)
        {
            _servicioGeocodificacion = servicioGeocodificacion;
            _eventBus = eventBus;
        }

        public async void Handle(EventoDeIntegracionDireccionAGeolocalizarCreada @event)
        {
            ResultadoGeocodificacion resultado = await _servicioGeocodificacion.Geocodificar(@event);

            var eventoGeoRealizada = new EventoDeIntegracionGeolocalizacionRealizada(
                @event.IdDireccion,
                resultado.Latitud,
                resultado.Longitud
                );

            _eventBus.Publicar(eventoGeoRealizada);
        }
    }
}