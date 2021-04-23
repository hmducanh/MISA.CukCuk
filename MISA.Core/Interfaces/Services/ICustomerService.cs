using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        // Lấy toàn bộ dữ liệu
        public IEnumerable<Customer> GetAll();
        // Lấy nhân viên bằng Id
        public Customer GetById(Guid customerId);
        // Thêm 1 nhân viên
        public int Insert(Customer customer);
        // Cập nhật thông tin của 1 nhân viên
        public int Update(Customer customer);
        // Xóa 1 nhân viên
        public int Delete(Guid customerId);
    }
}
