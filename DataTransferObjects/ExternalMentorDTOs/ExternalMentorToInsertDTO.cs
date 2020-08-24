using DataTransferObjects.ExternalMentorDTOs.ExMentorContact;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ExternalMentorDTOs
{
    public class ExternalMentorToInsertDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<MentorContactToInsertDTO> Contacts { get; set; }
    }
}
