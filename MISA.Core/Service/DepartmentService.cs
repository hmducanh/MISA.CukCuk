using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using MISA.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Service
{
    public class DepartmentService: BaseService<Department>, IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
    }
}
