using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Excepitons
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) was not found")
        {
            
        }
    }

}
