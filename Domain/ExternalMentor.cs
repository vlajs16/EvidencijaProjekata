using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ExternalMentor
    {
        public long CompanyID { get; set; }
        public int MentorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<ExternalMentorContact> Contacts { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;
            ExternalMentor m = (ExternalMentor)obj;
            if (this.CompanyID == m.CompanyID && this.MentorID == m.MentorID)
                return true;
            return false;
        }

    }
}
