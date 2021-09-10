namespace Geocodificador.Modelos
{
    public class ResultadoGeocodificacion
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public ResultadoGeocodificacion(double latitud, double longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
        }

    }
}
