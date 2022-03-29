using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ZOLLA.TestTask.Models
{
    internal class OrderRepository : IRepository<Order>
    {
        private readonly Context context;



        public OrderRepository(Context context)
        {
            this.context = context ?? throw new NullReferenceException();
        }



        public void Create(Order item)
        {
            if (item == null)
            {
                return;
            }

            context.Orders.Add(item);
        }

        public void Delete(int id)
        {
            var order = context.Orders.Find(id);

            if (order != null)
            {
                context.Orders.Remove(order);
            }
        }

        public IEnumerable<Order> Get()
        {
            return context.Orders;
        }

        public Order Get(int id)
        {
            return context.Orders.Find(id);
        }

        public void Update(Order item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}