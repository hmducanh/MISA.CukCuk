using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Service
{
    /// <summary>
    /// cac dich vu cho nhan vien
    /// </summary>
    /// CreatedBy : hmducanh (08/05/2021)
    public interface IEmployeeService : IBaseService<Employee>
    {
        public bool CheckEmployeeCodeExist(string employeeCode);
        public string GetMaximumEmployeeCode();
    }
}
