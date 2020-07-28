using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.EmployeePositionDTOs
{
    public class EmployeePositionDTO
    {
        public EmployeeDTO Employee { get; set; }
        public Position Position { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
