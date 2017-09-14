using System;
using System.Data.Entity;
using GlossaryDBContext.DBClasses;

namespace GlossaryDBContext.Repository
{
    public class UnitOfWork:IUnitOfWork,IDisposable
    {
        private readonly GlossaryDBContext _dbCOntext;
        private IGenericRepository<Glossary> _modelRepository;

        public UnitOfWork(GlossaryDBContext context)
        {
            _dbCOntext = context;
        }
        public UnitOfWork()
        {
            
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GlossaryDBContext>());
            _dbCOntext = new GlossaryDBContext();
        }
        public IGenericRepository<Glossary> ModelRepository
        {
            get { return _modelRepository ?? (_modelRepository = new GenericRepository<Glossary>(_dbCOntext)); }
        }

        public void Save()
        {
            _dbCOntext.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbCOntext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
