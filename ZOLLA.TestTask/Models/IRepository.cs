using System.Collections.Generic;

namespace ZOLLA.TestTask.Models
{
    internal interface IRepository<T> where T : class
    {
        void Create(T item);

        void Delete(int id);

        IEnumerable<T> Get();

        T Get(int id);

        void Update(T item);
    }
}