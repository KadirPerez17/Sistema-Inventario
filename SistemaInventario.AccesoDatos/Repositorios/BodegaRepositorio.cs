using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorios.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorios
{
    public class BodegaRepositorio : Repositorio<UnidadTrabajo>, IBodegaRepositorio
    {

        private readonly ApplicationDbContext _db;

        public BodegaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(UnidadTrabajo bodega)
        {
            var bodegaDb = _db.Bodegas.FirstOrDefault(b => b.Id == bodega.Id);
            if (bodegaDb == null)
            {
                throw new Exception("Bodega no encontrada");
            }
            else
            {
                bodegaDb.Nombre = bodega.Nombre;
                bodegaDb.Descripcion = bodega.Descripcion;
                bodegaDb.Estado = bodega.Estado;
                _db.SaveChanges();
            }
        }
    }
}
