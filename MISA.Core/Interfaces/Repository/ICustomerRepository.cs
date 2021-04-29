using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.core.Interfaces.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {/// <summary>
     /// thêm 1 số (2) thao tác với database
     /// </summary>
     /// CreatedBy : hmducanh (//)
        public bool CheckCustomerCodeExist(string customerCode);
        public bool CheckPhoneNumberExist(string phoneNumber);
    }
}