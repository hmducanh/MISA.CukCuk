using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Exceptions
{
    public class CustomerException : Exception
    {
        /// <summary>
        /// Trả về lỗi do mình định nghĩa
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        public CustomerException (string msg) : base(msg)
        {
            this.Data.Add("sunbae_mess", "loi do data gui len");
        }
    }
}
