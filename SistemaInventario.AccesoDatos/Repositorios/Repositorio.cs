using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorios.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db) 
        { 
            _db = db;
            this.dbSet = _db.Set<T>();
        }


        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad);
        }

        public async Task AgregarRango(IEnumerable<T> entidades)
        {
            await dbSet.AddRangeAsync(entidades);
        }

        public async Task<T> Obtener(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = false)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var propiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propiedad);
                }
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string incluirPropiedades = null, bool isTracking = false)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null) 
            { 
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var propiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propiedad);
                }
            }
            if (orderby != null) 
            { 
                query = orderby(query);
            }
            if (!isTracking) 
            { 
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidades)
        {
            dbSet.RemoveRange(entidades);
        }
    }
}
