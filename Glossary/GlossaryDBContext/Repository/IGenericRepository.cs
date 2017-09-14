using System;
using System.Linq;
using System.Linq.Expressions;

namespace GlossaryDBContext.Repository
{
    public interface IGenericRepository<TEntity>
    {
        //Get all operations
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity,object>>[] includes);
        //Get specific record item
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        IQueryable<TEntity> Query(Expression<Func<TEntity,bool>> filter=null,Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null);
    }
}
