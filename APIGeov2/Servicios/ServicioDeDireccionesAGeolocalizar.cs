using API.Data;
using API.Modelos;
using System;
using static API.Enum.DireccionEstado;

namespace API.Servicios
{
    public class ServicioDeDireccionesAGeolocalizar
    {
        private IDireccionRepositorio _repositorio;

        public ServicioDeDireccionesAGeolocalizar(IDireccionRepositorio repositorio) 
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        public Guid Crear(Direccion direccion)
        {
            var direccionBD = Direccion.Crear(
                new Guid(),
                DireccionEstadoEnum.PROCESANDO,
                direccion.Calle,
                direccion.Numero,
                direccion.Ciudad,
                direccion.CodigoPostal,
                direccion.Provincia,
                direccion.Pais
                );

            _repositorio.Crear(direccionBD);

            return direccionBD.Id;
        }

        public Direccion Obtener(Guid id)
        {
            return _repositorio.Obtener(id);
        }

        public void Actualizar(Guid id, double latitud, double longitud)
        {
            var direccion = _repositorio.Obtener(id);
            direccion.Latitud = latitud;
            direccion.Longitud = longitud;
            direccion.Estado = DireccionEstadoEnum.TERMINADO;

            _repositorio.GuardarCambios();
        }
    }
}
