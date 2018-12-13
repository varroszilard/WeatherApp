using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository.Base
{
    public interface IBaseRepository<T>
    {
        TableQuery<T> GetAll();
        void DeleteAll();
        T GetById(Guid id);
        void Insert(T value);
        void InsertAll(IEnumerable<T> list);
        void Delete(T value);
        void Update(T value);
        void CreateTable();
    }
}
