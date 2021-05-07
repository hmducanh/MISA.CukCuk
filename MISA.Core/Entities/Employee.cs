using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string GenderName
        {
            get
            {
                if (Gender == "0")
                    return "Nam";
                else if (Gender == "1")
                    return "Nữ";
                else
                    return "Khác";
            }
        }
        public string IdentifyNumber { get; set; }
        public DateTime DateCreateIN { get; set; }
        public string PlaceCreateIN { get; set; }
        public string PositionName { get; set; }
        public string Address { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string ConstantPhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }

    }
}
