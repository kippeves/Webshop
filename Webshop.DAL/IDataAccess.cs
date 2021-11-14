using System;
using System.Collections.Generic;

namespace Webshop.DAL
{
    public interface IDataAccess<T> {
        public T LoadById(int i);
        public IEnumerable<T> LoadAll();
        public void Save(T obj);
        public bool Update(T obj);
        public void Delete(T obj);
    }
}
