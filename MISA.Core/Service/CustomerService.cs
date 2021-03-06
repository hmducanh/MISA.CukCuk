using MISA.core.Interfaces.Repository;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Service;
using MISA.Core.Services;
using System;
using System.Collections.Generic;


namespace MISA.Core.Service
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        /// <summary>
        /// Các dịch riêng cho Customer
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }


    }
}