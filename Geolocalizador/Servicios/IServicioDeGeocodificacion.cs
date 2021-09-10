using Geocodificador.Eventos;
using Geocodificador.Modelos;
using System.Threading.Tasks;

namespace Geocodificador.Servicios
{
    public interface IServicioDeGeocodificacion
    {
        public abstract Task<ResultadoGeocodificacion> Geocodificar(EventoDeIntegracionDireccionAGeolocalizarCreada evento);
    }
}