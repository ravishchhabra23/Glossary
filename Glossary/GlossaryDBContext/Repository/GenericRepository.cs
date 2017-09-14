using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace GlossaryDBContext.Repository
{
    public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity:class
    {
        private DbContext _dbContext;
        private DbSet<TEntity> _dbset;
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            this._dbset = dbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity,object>>[] includes)
        {
            IQueryable<TEntity> query = _dbset;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query;

        }
        public void Insert(TEntity entity)
        {
            _dbset.Add(entity);
        }
        public void Update(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                var id = _dbset.Create().GetType().GetProperty("TermId").GetValue(entity);
                var set = _dbContext.Set<TEntity>();
                var attached = set.Find(id);
                if (attached != null)
                {
                    var attachedEntry = _dbContext.Entry(attached);
                    attachedEntry.CurrentValues.SetValues(entity);
                    //_dbContext.Entry(attached).CurrentValues.SetValues(entity);
                }
                else
                    _dbContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _dbset.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }
        public void Delete(object id)
        {
            TEntity entityToDelete = _dbset.Find(id);
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbset.Attach(entityToDelete);
            }
            _dbset.Remove(entityToDelete);

        }
        public TEntity GetByID(object id)
        {
            TEntity entityTOReturn = _dbset.Find(id);
            return entityTOReturn;
        }
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbset;
            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                query = orderBy(query);
            return query;
        }
    }
}
