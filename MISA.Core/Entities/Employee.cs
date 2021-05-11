using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    public class Employee
    {
        /// <summary>
        /// thong tin nhan vien
        /// </summary>
        /// CreatedBy : hmducanh (08/05/2021)
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string GenderName
        {
            get
            {
                if (Gender == 0)
                    return "Nam";
                else if (Gender == 1)
                    return "Nữ";
                else
                    return "Khác";
            }
        }
        public string IdentifyNumber { get; set; }
        public DateTime IdentifyDate { get; set; }
        public string IdentifyPlace { get; set; }
        public string PositionName { get; set; }
        public string DepartmentName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ConstantPhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }

    }
}
