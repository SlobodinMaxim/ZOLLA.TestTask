using System;
using System.Web.Mvc;

namespace ZOLLA.TestTask.Models
{
    internal class UnitOfWork : IDisposable
    {
        private readonly Context context;

        private bool disposed = false;



        public IRepository<Client> Clients { get; private set; }

        public IRepository<Order> Orders { get; private set; }



        public UnitOfWork()
        {
            context = new Context();
            Clients = DependencyResolver.Current.GetService<IRepository<Client>>();
            Orders = DependencyResolver.Current.GetService<IRepository<Order>>();
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