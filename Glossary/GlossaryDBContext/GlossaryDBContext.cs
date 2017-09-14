using System.Data.Entity;
using GlossaryDBContext.DBClasses;

namespace GlossaryDBContext
{
    public class GlossaryDBContext:DbContext
    {
        public GlossaryDBContext(): base("name=GlossaryDBContext")
        {
        }
        public DbSet<Glossary> Glossary { get; set; }
    }
}
