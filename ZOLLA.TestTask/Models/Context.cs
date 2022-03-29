using System.Data.Entity;

namespace ZOLLA.TestTask.Models
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }



        public Context() : base("ZOLLATestTask")
        {

        }
    }
}