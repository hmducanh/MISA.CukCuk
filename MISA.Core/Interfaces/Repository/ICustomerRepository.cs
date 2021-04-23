using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        // Lấy toàn bộ dữ liệu
        public IEnumerable<Customer> GetAll();
        // Lấy nhân viên bằng Id
        public Customer GetById(Guid csustomerId);
        // Thêm 1 nhân viên
        public int Insert(Customer customer);
        // Cập nhật thông tin của 1 nhân viên
        public int Update(Customer customer);
        // Xóa 1 nhân viên
        public int Delete(Guid customerId);
        // kiểm tra trùng mã nhân viên
        public bool CheckCustomerCodeExist(string customerCode);
        // kiểm tra trùng số điện thoại
        public bool CheckPhoneNumberExist(string phoneNumber);
    }
}
