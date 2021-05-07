using MISA.core.Interfaces.Repository;
using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        // ktra trung ma nhan vien
        public bool CheckEmployeeCodeExist(string customerCode);

        public string GetMaximumEmployeeCode();
    }
}
