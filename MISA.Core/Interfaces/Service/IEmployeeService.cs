using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Service
{
    // cac dich vu cho nhan vien
    public interface IEmployeeService : IBaseService<Employee>
    {
        public string GetMaximumEmployeeCode();
    }
}
