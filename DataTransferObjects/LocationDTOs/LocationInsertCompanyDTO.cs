using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.LocationDTOs
{
    public class LocationInsertCompanyDTO
    {
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Floor { get; set; }
        public string AppartmentNumber { get; set; }
        public City City { get; set; }
    }
}
