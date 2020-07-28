using Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ExternalMentorDTOs.ExMentorContact
{
    public class MentorContactToInsertDTO
    {
        public int? SerialNumber { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
    }
}
