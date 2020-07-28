using System;
using System.Collections.Generic;

namespace Domain
{
    public class Company
    {
        public long CompanyID { get; set; }
        public string Name { get; set; }
        public string CompanyUsername { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Location> Locations { get; set; }
        public List<ExternalMentor> Mentors { get; set; }
    }
}
