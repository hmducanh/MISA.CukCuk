using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.core.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Base của Repository - các thao tác với database chung 
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        public IEnumerable<T> GetAll();
        public T GetById(Guid EntityId);
        public int Insert(T Entity);
        public int Update(T Entity);
        public int Delete(Guid EntityId);
    }
}