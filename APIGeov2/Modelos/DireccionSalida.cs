using API.Enum;
using System;

namespace API.Modelos
{
    public class DireccionSalida
    {
        public Guid Id { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string Estado { get; set; }

        public DireccionSalida(Guid id, double? latitud, double? longitud, string estado)
        {
            Id = id;
            Latitud = latitud;
            Longitud = longitud;
            Estado = estado;
        }        

        public static DireccionSalida ADireccionSalida(Direccion direccion)
        {
            return new DireccionSalida(
                direccion.Id, 
                direccion.Latitud, 
                direccion.Longitud,
                DireccionEstado.ToString(direccion.Estado)
                );
        }
    }
}
