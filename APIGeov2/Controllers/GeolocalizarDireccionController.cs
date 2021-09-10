using API.Eventos;
using API.Modelos;
using API.Servicios;
using EventBusv2.Base;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class GeolocalizarDireccionController : Controller
    {
        private ServicioDeDireccionesAGeolocalizar _servicioDireccionesAGeolocalizar;
        private IEventBus _eventBus;

        public GeolocalizarDireccionController(ServicioDeDireccionesAGeolocalizar servicio, IEventBus eventBus)
        {
            _servicioDireccionesAGeolocalizar = servicio ?? throw new ArgumentNullException(nameof(servicio));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }
        
        [SwaggerOperation()]
        [Route("geolocalizar")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult> Post([FromBody]Direccion direccion)
        {            
            var direccionId = _servicioDireccionesAGeolocalizar.Crear(direccion);
            
            var evento = new EventoDeIntegracionDireccionAGeolocalizarCreada(
                direccionId, 
                direccion.Calle, 
                direccion.Numero, 
                direccion.Ciudad, 
                direccion.CodigoPostal, 
                direccion.Provincia, 
                direccion.Pais
                );

            _eventBus.Publicar(evento);

            return Accepted(new { Id = direccionId });
        }


        [SwaggerOperation()]
        [Route("geocodificar")]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]Guid id)
        {
            Direccion direccion = _servicioDireccionesAGeolocalizar.Obtener(id);

            return Ok(DireccionSalida.ADireccionSalida(direccion));
        }
    }
}
