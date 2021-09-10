using System;
using static API.Enum.DireccionEstado;

namespace API.Modelos
{
    public class Direccion
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid Id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public DireccionEstadoEnum Estado { get; set; }

        public string Calle { get; set; }

        public int Numero { get; set; }

        public string Ciudad { get; set; }

        public int CodigoPostal { get; set; }

        public string Provincia { get; set; }

        public string Pais { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public double? Latitud { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public double? Longitud { get; set; }


        public Direccion()
        {
        }

        private Direccion(Guid id, DireccionEstadoEnum estado, string calle, int numero, string ciudad, int codigoPostal, string provincia, string pais, double? latitud = null, double? longitud = null)
        {
            Id = id;
            Estado = estado;
            Calle = calle;
            Numero = numero;
            Ciudad = ciudad;
            CodigoPostal = codigoPostal;
            Provincia = provincia;
            Pais = pais;
            Latitud = latitud;
            Longitud = longitud;
        }
        

        public static Direccion Crear(Guid id, DireccionEstadoEnum estado, string calle, int numero, string ciudad, int codigoPostal, string provincia, string pais)
        {
            return new Direccion(id, 
                estado, 
                calle, 
                numero, 
                ciudad, 
                codigoPostal, 
                provincia, 
                pais);
        }
        
    }
}
