using System;
using GlossaryDBContext.DBClasses;

namespace GlossaryDBContext.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        void Save();
        IGenericRepository<Glossary> ModelRepository { get; }
    }
}
