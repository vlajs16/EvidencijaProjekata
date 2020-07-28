using DataTransferObjects.CompanyDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ProjectProposalDTOs
{
    public class ProjectProposalForListDTO
    {
        public long ProjectProposalID { get; set; }
        public string Name { get; set; }
        public DateTime ProposalDate { get; set; }
        public string Description { get; set; }
        public CompanyForListDTO Company { get; set; }
    }
}
