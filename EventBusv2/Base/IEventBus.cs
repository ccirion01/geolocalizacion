namespace EventBusv2.Base
{
    public interface IEventBus
    {
        public void Suscribir<T, TH>(TH @handler)
            where T : EventoDeIntegracion
            where TH : IEventoDeIntegracionHandler<T>;
        public void Publicar(EventoDeIntegracion @event);
    }
}
