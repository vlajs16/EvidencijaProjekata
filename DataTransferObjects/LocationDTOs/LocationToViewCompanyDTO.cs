using DataTransferObjects.CityDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.LocationDTOs
{
    public class LocationToViewCompanyDTO
    {
        public CityToListDTO City { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Floor { get; set; }
        public string AppartmentNumber { get; set; }
    }
}
