using System;
using System.Collections.Generic;

namespace EventBusv2.Base
{
    public interface IGestorDeSuscripciones
    {
        void AgregarSuscripcion<T, TH>(TH @handler)
           where T : EventoDeIntegracion
           where TH : IEventoDeIntegracionHandler<T>;

        bool HaySuscripcionesParaEvento(string eventName);

        Type ObtenerTipoDeEventoPorNombre(string eventName);

        IEnumerable<SuscripcionHandler> ObtenerHandlersDeEvento(string eventName);

        string GetEventKey<T>();

        bool HandlerYaRegistrado(Type tipoEvento, Type tipoHandler);
    }
}
