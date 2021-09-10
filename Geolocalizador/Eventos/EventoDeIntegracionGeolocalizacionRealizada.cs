using EventBusv2.Base;
using System;

namespace Geocodificador.Eventos
{
    public class EventoDeIntegracionGeolocalizacionRealizada : EventoDeIntegracion
    {
        public Guid IdDireccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public EventoDeIntegracionGeolocalizacionRealizada(Guid idDireccion, double latitud, double longitud) : base()
        {
            IdDireccion = idDireccion;
            Latitud = latitud;
            Longitud = longitud;
        }
    }
}
