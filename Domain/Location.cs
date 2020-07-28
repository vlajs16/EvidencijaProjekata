using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Location
    {
        public long CompanyID { get; set; }
        public Company Company { get; set; }
        public long CityID { get; set; }
        public City City { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Floor { get; set; }
        public string AppartmentNumber { get; set; }
    }
}
