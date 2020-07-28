using DataTransferObjects.ProjectProposalDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ProjectDTOs
{
    public class ProjectsForListDTO
    {
        public long ProjectID { get; set; }
        public ProjectProposalProjectToListDTO ProjectProposal { get; set; }
    }
}
