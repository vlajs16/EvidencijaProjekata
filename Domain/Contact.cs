using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public enum ContactType
    {
        Email,
        Telephone,
        LinkedIn
    }
    public class Contact
    {
        public long CompanyID { get; set; }
        public long ContactID { get; set; }
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
    }
}
