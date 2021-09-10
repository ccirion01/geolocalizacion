using EventBusv2;
using EventBusv2.Base;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace RabbitMQEventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private string _nombreBroker;
        private string _qNombre;
        private readonly IConnectionFactory _factoryConexion;
        private IModel _canalConsumidor;
        private readonly IGestorDeSuscripciones _gestorSuscripciones;
        IConnection _conexion;

        public EventBusRabbitMQ(IConnectionFactory factoryConexion, IGestorDeSuscripciones gestorSuscripciones, string exchange, string qNombre = null)
        {
            _factoryConexion = factoryConexion ?? throw new ArgumentNullException(nameof(factoryConexion));
            _conexion = _factoryConexion.CreateConnection();
            _nombreBroker = exchange;
            _qNombre = qNombre;
            _canalConsumidor = CrearCanalConsumidor();
            _gestorSuscripciones = gestorSuscripciones ?? throw new ArgumentNullException(nameof(gestorSuscripciones));
        }

        public void Suscribir<T, TH>(TH @handler)
            where T : EventoDeIntegracion
            where TH : IEventoDeIntegracionHandler<T>
        {
            var nombreEvento = _gestorSuscripciones.GetEventKey<T>();

            _canalConsumidor.QueueBind(queue: _qNombre,
                                    exchange: _nombreBroker,
                                    routingKey: nombreEvento);

            _gestorSuscripciones.AgregarSuscripcion<T, TH>(@handler);
            IniciarConsumoBasico();
        }

        public void Publicar(EventoDeIntegracion @evento)
        {
            var nombreEvento = @evento.GetType().Name;

            using (var canal = _conexion.CreateModel())
            {
                canal.ExchangeDeclare(exchange: _nombreBroker, type: "direct");

                var body = JsonSerializer.SerializeToUtf8Bytes(@evento, @evento.GetType(), new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                canal.BasicPublish(
                        exchange: _nombreBroker,
                        routingKey: nombreEvento,
                        basicProperties: null,
                        body: body);
            }
        }

        private IModel CrearCanalConsumidor()
        {
            var canal = _conexion.CreateModel();

            canal.ExchangeDeclare(exchange: _nombreBroker,
                                    type: "direct");

            canal.QueueDeclare(queue: _qNombre,
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            canal.CallbackException += (sender, ea) =>
            {
                _canalConsumidor.Dispose();
                _canalConsumidor = CrearCanalConsumidor();
                IniciarConsumoBasico();
            };

            return canal;
        }

        private void IniciarConsumoBasico()
        {
            if (_canalConsumidor != null)
            {
                var consumidor = new EventingBasicConsumer(_canalConsumidor);
                consumidor.Received += Consumidor_Recibido;

                _canalConsumidor.BasicConsume(
                    queue: _qNombre,
                    autoAck: false,
                    consumer: consumidor);
            }
        }

        private void Consumidor_Recibido(object sender, BasicDeliverEventArgs eventArgs)
        {
            var nombreEvento = eventArgs.RoutingKey;
            var mensaje = Encoding.UTF8.GetString(eventArgs.Body.Span);

            Procesar(nombreEvento, mensaje);

            _canalConsumidor.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private void Procesar(string nombreEvento, string mensaje)
        {
            if (_gestorSuscripciones.HaySuscripcionesParaEvento(nombreEvento))
            {
                var suscripciones = _gestorSuscripciones.ObtenerHandlersDeEvento(nombreEvento);
                foreach (SuscripcionHandler suscripcion in suscripciones)
                {                    
                    Type tipoEvento = _gestorSuscripciones.ObtenerTipoDeEventoPorNombre(nombreEvento);
                    var evento = JsonSerializer.Deserialize(mensaje, tipoEvento, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    Type tipoConcreto = typeof(IEventoDeIntegracionHandler<>).MakeGenericType(tipoEvento);

                    tipoConcreto.GetMethod("Handle").Invoke(suscripcion.Handler, new object[] { evento });
                }
            }            
        }
    }
}
