using System;

namespace ZOLLA.TestTask.Models
{
    internal class UnitOfWork : IDisposable
    {
        private readonly Context context;

        private bool disposed = false;



        public ClientRepository Clients { get; private set; }

        public OrderRepository Orders { get; private set; }



        public UnitOfWork()
        {
            context = new Context();
            Clients = new ClientRepository(context);
            Orders = new OrderRepository(context);
        }



        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}