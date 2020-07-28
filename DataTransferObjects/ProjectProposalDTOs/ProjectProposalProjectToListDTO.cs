using DataTransferObjects.CompanyDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ProjectProposalDTOs
{
    public class ProjectProposalProjectToListDTO
    {
        public string Name { get; set; }
        public CompanyProjectToListDTO Company { get; set; }
    }
}
