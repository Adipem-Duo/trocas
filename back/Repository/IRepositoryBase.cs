using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Data;

namespace back {

    public interface IRepositoryBase<T> : IDisposable 
    {
        ICollection<T> GetAlls();
        T GetById (int id);
        void Insert(T t);
        void Delete(int id);
        void Update(T student);
        void Save();
        void dispose(); 
    }
}