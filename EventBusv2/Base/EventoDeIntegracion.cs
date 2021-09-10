using System;
using System.Text.Json.Serialization;

namespace EventBusv2.Base
{
    public abstract class EventoDeIntegracion
    {
        public EventoDeIntegracion()
        {
            Id = Guid.NewGuid();
            FechaDeCreacion = DateTime.UtcNow;
        }

        [JsonConstructor]
        public EventoDeIntegracion(Guid id, DateTime fechaDeCreacion)
        {
            Id = id;
            FechaDeCreacion = fechaDeCreacion;
        }

        [JsonInclude]
        public Guid Id { get; private set; }

        [JsonInclude]
        public DateTime FechaDeCreacion { get; private set; }
    }
}
