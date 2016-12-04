using System.Data.Entity;

namespace Vrhw.Repository.Sql.Entities
{
    public class VrhwContext : DbContext
    {
        public VrhwContext()
            : base("VrhwContext")
        {
        }

        public DbSet<Diff> Diff { get; set; }
    }
}