using DAL.Connection;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public BaseRepository()
        {
        }

        public void Delete(T value)
        {
            DBConnection.GetConnection().Delete<T>(value);
        }

        public TableQuery<T> GetAll()
        {
            return DBConnection.GetConnection().Table<T>();
        }

        public T GetById(Guid id)
        {
            return DBConnection.GetConnection().Get<T>(id);
        }

        public void Insert(T value)
        {
            DBConnection.GetConnection().Insert(value);
        }

        public void Update(T value)
        {
            DBConnection.GetConnection().InsertOrReplace(value);
        }

        public void CreateTable()
        {
            DBConnection.GetConnection().CreateTable<T>();
        }

        public void DeleteAll()
        {
            DBConnection.GetConnection().DeleteAll<T>();
        }

        public void InsertAll(IEnumerable<T> list)
        {
            DBConnection.GetConnection().InsertAll(list);
        }
    }
}
