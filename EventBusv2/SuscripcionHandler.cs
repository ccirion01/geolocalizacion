using EventBusv2.Base;
using System;

namespace EventBusv2
{
    public class SuscripcionHandler
    {
        public Type Tipo { get; set; }
        public IEventoDeIntegracionHandler Handler { get; set; }

        public SuscripcionHandler(Type tipo, IEventoDeIntegracionHandler handler)
        {
            Tipo = tipo;
            Handler = handler;
        }
    }
}
