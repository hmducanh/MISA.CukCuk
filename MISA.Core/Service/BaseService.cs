using MISA.core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        /// <summary>
        /// chi tiết các dịch vụ được cung cấp
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        IBaseRepository<T> _baseRepository;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        // xóa 1 thực thể
        public int Delete(Guid EntityID)
        {
            var rowAffect = _baseRepository.Delete(EntityID);
            return rowAffect;
        }
        // lấy tất cả thực thể
        public IEnumerable<T> GetAll()
        {
            var customers = _baseRepository.GetAll();
            return customers;
        }
        // lấy thực thể theo id
        public T GetById(Guid EntityID)
        {
            var customer = _baseRepository.GetById(EntityID);
            return customer;
        }
        // thêm 1 thực thể
        public int Insert(T Entity)
        {
            validate(Entity);
            var rowAffect = _baseRepository.Insert(Entity);
            return rowAffect;
        }
        // kiểm tra dữ liệu
        protected virtual void validate(T Entity) { }
        // cập nhật dữ liệu ( thực thể )
        public int Update(T Entity)
        {
            var rowAffect = _baseRepository.Update(Entity);
            return rowAffect;
        }
    }
}