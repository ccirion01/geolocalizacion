using System.Threading.Tasks;

namespace EventBusv2.Base
{
    public interface IEventoDeIntegracionHandler<in TEventoDeIntegracion> : IEventoDeIntegracionHandler 
        where TEventoDeIntegracion : EventoDeIntegracion
    {
        void Handle(TEventoDeIntegracion @event);
    }

    public interface IEventoDeIntegracionHandler
    {
    }
}
