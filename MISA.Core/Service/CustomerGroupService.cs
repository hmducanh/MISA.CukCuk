using MISA.core.Interfaces.Repository;
using MISA.Core.Entities;
using MISA.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Service
{
    public class CustomerGroupService : BaseService<CustomerGroup>, ICustomerGroupService
    {
        /// <summary>
        /// 
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        ICustomerGroupRepository _customerGroupRepository;

        public CustomerGroupService(ICustomerGroupRepository customerGroupRepository) : base(customerGroupRepository)
        {
            _customerGroupRepository = customerGroupRepository;
        }

    }
}