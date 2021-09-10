using Geocodificador.Eventos;
using Geocodificador.Http;
using Geocodificador.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geocodificador.Servicios
{
    public class ServicioDeGeocodificacionOSM : IServicioDeGeocodificacion
    {
        private readonly string Uri = "http://nominatim.openstreetmap.org/search";

        public async Task<ResultadoGeocodificacion> Geocodificar(EventoDeIntegracionDireccionAGeolocalizarCreada evento)
        {
            //try
            //{            
            var headers = new Dictionary<string, string>
            {
                ["User-Agent"] = "Geocodificador"
            };

            var queryStrings = new Dictionary<string, string>
            {
                ["street"] = $"{evento.Numero} {evento.Calle}",
                ["city"] = evento.Ciudad,
                ["postalcode"] = evento.CodigoPostal.ToString(),
                ["state"] = evento.Provincia,
                ["country"] = evento.Pais,
                ["format"] = "json",
            };

            var valores = await ClienteHttp
            .GetAsync<List<Dictionary<string, object>>>(Uri, headers, queryStrings);

            return ResultadoGeocodificacionOSM.CrearDesdeDiccionario(valores.First());
            //}
            //catch(Exception ex)
            //{
            //    //Loguear error.
            //}
        }

    }
}