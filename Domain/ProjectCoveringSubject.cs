using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ProjectCoveringSubject
    {
        public long ProjectProposalID { get; set; }
        public long ProjectCoveringSubjectID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ScientificArea ScientificArea { get; set; }
    }
}
