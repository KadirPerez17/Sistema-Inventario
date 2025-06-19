using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorios.IRepositorio
{
    public  interface IRepositorio<T> where T : class
    {
        Task<T> Obtener(int id);
        Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            String incluirPropiedades = null,
            bool isTracking = false
            );

        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,
            String incluirPropiedades = null,
            bool isTracking = false
            );

        Task Agregar(T entidad);

        Task AgregarRango(IEnumerable<T> entidades);

        void Remover(T entidad);

        void RemoverRango(IEnumerable<T> entidades);

    }
}
