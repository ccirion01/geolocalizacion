using System.Collections.Generic;

namespace Geocodificador.Modelos
{
    public class ResultadoGeocodificacionOSM : ResultadoGeocodificacion
    {   
        public ResultadoGeocodificacionOSM(double latitud, double longitud) : base(latitud, longitud)
        {
        }

        public static ResultadoGeocodificacionOSM CrearDesdeDiccionario(Dictionary<string, object> datos)
        {
            return new ResultadoGeocodificacionOSM(
                double.Parse(datos["lat"].ToString()),
                double.Parse(datos["lon"].ToString())
                );
        }
    }
}
