using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ExternalMentorContact
    {
        public long ExternalMentorCompanyID { get; set; }
        public int ExternalMentorMentorID { get; set; }
        public int SerialNumber { get; set; }
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
    }
}
