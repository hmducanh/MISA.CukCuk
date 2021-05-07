﻿using MISA.core.Interfaces.Repository;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using MISA.Core.Services;
using System;
using System.Collections.Generic;


namespace MISA.Core.Service
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        /// <summary>
        /// Các dịch riêng cho Employee
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public string GetMaximumEmployeeCode()
        {
            string res = _employeeRepository.GetMaximumEmployeeCode();
            string result = "";
            int pos = res.Length;
            for(int i = res.Length - 1; i >= 2; i--)
            {
                if (res[i] == '9')
                    result = "0" + result;
                else
                {
                    pos = i;
                    result = (res[i] - '0' + 1).ToString() + result;
                    break;
                }
            }
            if(pos == res.Length)
            {
                result = "NV1" + result;
            }
            else
            {
                for (int i = pos - 1; i >= 0; i--)
                    result = res[i] + result;
            }
            return result;
                
        }
    }
}