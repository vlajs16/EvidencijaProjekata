using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ProjectProposal
    {
        public long ProjectProposalID { get; set; }
        public DateTime ProposalDate { get; set; }
        public string Name { get; set; }
        public string Goal { get; set; }
        public string Description { get; set; }
        public string Activities { get; set; }
        public DateTime StartDateProjectProposal { get; set; }
        public int DaysDuration { get; set; }
        public ExternalMentor ExternalMentor { get; set; }
        public Company Company { get; set; }
        public List<ProjectCoveringSubject> Subjects { get; set; }
        public bool Approved { get; set; } = false;
    }
}
