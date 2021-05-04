using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.AttributeCustom
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute 
    {
        public string MsgError = string.Empty; 
        public MISARequired(string msgError = "")
        {
            MsgError = msgError;
        }
    }
}
