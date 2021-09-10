using API.Modelos;
using System;

namespace API.Data.EF
{
    public class DireccionRepositorioEF : IDireccionRepositorio
    {
        private DireccionDbContext _dbContext;

        public DireccionRepositorioEF(DireccionDbContext dbContext)
        {
            this._dbContext = dbContext;
        }        

        public void Crear(Direccion direccion)
        {
            _dbContext.Direcciones.Add(direccion);
            GuardarCambios();
        }

        public Direccion Obtener(Guid id)
        {
            return _dbContext.Direcciones.Find(id);
        }

        public void GuardarCambios()
        {
            _dbContext.SaveChanges();
        }
    }
}
