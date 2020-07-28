using Domain;
using System;
using System.Collections.Generic;

namespace DataTransferObjects
{
    public class EmployeeDTO
    {
        public long EmployeeID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
