using EventBusv2.Base;
using System;

namespace API.Eventos
{
    public class EventoDeIntegracionDireccionAGeolocalizarCreada : EventoDeIntegracion
    {
        public Guid IdDireccion { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }
        public string Ciudad { get; set; }
        public int CodigoPostal { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }

        public EventoDeIntegracionDireccionAGeolocalizarCreada(Guid idDireccion, string calle, int numero, string ciudad, int codigoPostal, string provincia, string pais) : base()
        {
            IdDireccion = idDireccion;
            Calle = calle;
            Numero = numero;
            Ciudad = ciudad;
            CodigoPostal = codigoPostal;
            Provincia = provincia;
            Pais = pais;
        }
    }
}
