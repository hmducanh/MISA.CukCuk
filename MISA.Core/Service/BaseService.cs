using MISA.core.Interfaces.Repository;
using MISA.Core.AttributeCustom;
using MISA.Core.Exceptions;
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
        // kiểm tra dữ liệu
        private void Validate(T Entity)
        {
            // lay ra tat ca cac property cua class Entity
                var properties = typeof(T).GetProperties();
            // tim property can thuc hien ktra not null or empty
            foreach (var property in properties)
            {
                var requiredProperties = property.GetCustomAttributes(typeof(MISARequired), true);
                // neu co 
                if (requiredProperties.Length > 0)
                {
                    var propertyValue = property.GetValue(Entity);
                    // kiem tra rong
                    if (string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        // lay ten bo phan can kiem tra trong thuc the
                        var msgError = (requiredProperties[0] as MISARequired).MsgError;
                        if(!string.IsNullOrEmpty(msgError))
                        {
                            throw new CustomerException($"{msgError} không được phép để trống");
                        }
                    }
                }
            }
        }
        // thêm 1 thực thể
        public int Insert(T Entity)
        {
            Validate(Entity);
            var rowAffect = _baseRepository.Insert(Entity);
            return rowAffect;
        }
        
        // cập nhật dữ liệu ( thực thể )
        public int Update(T Entity)
        {
            var rowAffect = _baseRepository.Update(Entity);
            return rowAffect;
        }
    }
}