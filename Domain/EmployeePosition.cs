using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class EmployeePosition
    {
        public long EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public long PositionID { get; set; }
        public Position Position { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
