using API.Servicios;
using EventBusv2.Base;
using System;

namespace API.Eventos
{
    public class EventoDeIntegracionGeolocalizacionRealizadaHandler : IEventoDeIntegracionHandler<EventoDeIntegracionGeolocalizacionRealizada>
    {
        private ServicioDeDireccionesAGeolocalizar _servicioDireccionesAGeolocalizar;

        public EventoDeIntegracionGeolocalizacionRealizadaHandler(ServicioDeDireccionesAGeolocalizar servicio)
        {
            _servicioDireccionesAGeolocalizar = servicio ?? throw new ArgumentNullException(nameof(servicio));
        }

        public async void Handle(EventoDeIntegracionGeolocalizacionRealizada @event)
        {
            _servicioDireccionesAGeolocalizar.Actualizar(@event.IdDireccion, @event.Latitud, @event.Longitud);
        }
    }
}
