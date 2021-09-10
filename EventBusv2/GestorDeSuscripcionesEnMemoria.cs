using EventBusv2.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBusv2
{
    public class GestorDeSuscripcionesEnMemoria : IGestorDeSuscripciones
    {
        private readonly Dictionary<string, List<SuscripcionHandler>> _handlers;
        private readonly List<Type> _tiposDeEventos;

        public GestorDeSuscripcionesEnMemoria()
        {
            _handlers = new Dictionary<string, List<SuscripcionHandler>>();
            _tiposDeEventos = new List<Type>();
        }

        public void AgregarSuscripcion<T, TH>(TH @handler)
            where T : EventoDeIntegracion
            where TH : IEventoDeIntegracionHandler<T>
        {
            var nombreEvento = GetEventKey<T>();

            if (!HaySuscripcionesParaEvento(nombreEvento))
                _handlers.Add(nombreEvento, new List<SuscripcionHandler>());

            if (!HandlerYaRegistrado(typeof(T), typeof(TH)))
            {
               _handlers[nombreEvento].Add(new SuscripcionHandler(typeof(TH), @handler));

               if (!_tiposDeEventos.Contains(typeof(T)))
                    _tiposDeEventos.Add(typeof(T));
            }
        }

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        public Type ObtenerTipoDeEventoPorNombre(string nombreEvento) => _tiposDeEventos.SingleOrDefault(t => t.Name == nombreEvento);


        public IEnumerable<SuscripcionHandler> ObtenerHandlersDeEvento(string nombreEvento) => _handlers[nombreEvento];

        public bool HandlerYaRegistrado(Type tipoEvento, Type tipoHandler)
        {
            return _handlers[tipoEvento.Name].Any(s => s.Tipo == tipoHandler);
        }

        public bool HaySuscripcionesParaEvento(string nombreEvento) => _handlers.ContainsKey(nombreEvento);
    }
}
