using API.Modelos;
using System;

namespace API.Data
{
    public interface IDireccionRepositorio
    {
        public void Crear(Direccion direccion);

        public Direccion Obtener(Guid id);

        public void GuardarCambios();
    }
}
