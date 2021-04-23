using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using MISA.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MISA.Core.Service
{
    public class CustomerService : ICustomerService
    {
        ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public int Delete(Guid customerId)
        {
            var rowAffect = _customerRepository.Delete(customerId);
            return rowAffect;
        }

        public IEnumerable<Customer> GetAll()
        {
            var customers = _customerRepository.GetAll();
            return customers;
        }

        public Customer GetById(Guid customerId)
        {
            var customer = _customerRepository.GetById(customerId);
            return customer;
        }

        public int Insert(Customer customer)
        {
            // Valid du lieu

            // bo trong ma khach hang
            if(string.IsNullOrEmpty(customer.CustomerCode))
            {
                throw new CustomerException("Ma khach hang khong duoc de trong");   
            }

            // trung ma khach hang
            var isExists = _customerRepository.CheckCustomerCodeExist(customer.CustomerCode);
            if(isExists == true)
            {
                throw new CustomerException("Ma khach hang da ton tai trong he thong");
            }

            //Email khong hop le
            var emailTemplate = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if(Regex.IsMatch(customer.Email, emailTemplate) == false)
            {
                throw new CustomerException("Email khong dung dinh dang");
            }

            var rowAffect = _customerRepository.Insert(customer);
            return rowAffect;
        }

        public int Update(Customer customer)
        {
            var rowAffect = _customerRepository.Update(customer);
            return rowAffect;
        }
    }
}
