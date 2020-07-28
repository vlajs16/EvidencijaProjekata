using Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.CompanyContactDTOs
{
    public class ContactToInsertDTO
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
    }
}
