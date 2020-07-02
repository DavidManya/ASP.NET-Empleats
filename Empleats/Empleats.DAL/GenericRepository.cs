using Common.Lib.Core.Context;
using System;
using Common.Lib.Core;
using Common.Lib.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Empleats.DAL
{
    public class GenericRepository<T> : IRepository<T> where T : Entity
    {
        DbContext DbContext { get; set; }

        DbSet<T> DbSet
        {
            get
            {
                return DbContext.Set<T>();
            }
        }

        public GenericRepository()
        {

        }

        public GenericRepository(DbContext context)
        {
            DbContext = context;
        }

        public T Find(Guid id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public IQueryable<T> QueryAll()
        {
            return DbSet.AsQueryable();
        }

        public virtual SaveValidation<T> Add(T entity)
        {
            var output = new SaveValidation<T>()
            {
                IsSuccess = true
            };

            if (entity.Id == default(Guid))
                entity.Id = Guid.NewGuid();

            if (DbSet.Any(x => x.Id == entity.Id))
            {
                output.IsSuccess = false;
                output.Validation.Errors.Add("Ya existe una entidad con ese id");
            }

            if (output.IsSuccess)
            {
                DbSet.Add(entity);
                DbContext.SaveChanges();
            }

            return output;
        }

        public virtual SaveValidation<T> Update(T entity)
        {
            var output = new SaveValidation<T>()
            {
                IsSuccess = true
            };

            if (entity.Id == default(Guid))
            {
                output.IsSuccess = false;
                output.Validation.Errors.Add("No se puede actualizar una entidad sin Id");
            }

            if (entity.Id != default(Guid) && DbSet.All(x => x.Id != entity.Id))
            {
                output.IsSuccess = false;
                output.Validation.Errors.Add("No existe una entity con ese id");
            }

            if (output.IsSuccess)
            {
                DbSet.Update(entity);
                DbContext.SaveChanges();

            }

            return output;
        }

        public virtual DeleteValidation<T> Delete(T entity)
        {
            var output = new DeleteValidation<T>()
            {
                IsSuccess = true
            };

            if (DbSet.All(x => x.Id != entity.Id))
            {
                output.IsSuccess = false;
                output.Validation.Errors.Add("No existe una entity con ese id");
            }

            if (output.IsSuccess)
            {
                DbSet.Remove(entity);
                DbContext.SaveChanges();

            }

            return output;
        }
    }
}
