using System.Collections.Generic;
using System.Data.Entity;
using System;

namespace ZOLLA.TestTask.Models
{
    internal class ClientRepository : IRepository<Client>
    {
        private readonly Context context;



        public ClientRepository(Context context)
        {
            this.context = context ?? throw new NullReferenceException();
        }



        public void Create(Client item)
        {
            if (item == null)
            {
                return;
            }

            context.Clients.Add(item);
        }

        public void Delete(int id)
        {
            var client = context.Clients.Find(id);

            if (client != null)
            {
                context.Clients.Remove(client);
            }
        }

        public IEnumerable<Client> Get()
        {
            return context.Clients;
        }

        public Client Get(int id)
        {
            return context.Clients.Find(id);
        }

        public void Update(Client item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}