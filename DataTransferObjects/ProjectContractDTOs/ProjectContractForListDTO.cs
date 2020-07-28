using DataTransferObjects.ProjectDTOs;
using DataTransferObjects.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ProjectContractDTOs
{
    public class ProjectContractForListDTO
    {
        public long ProjectContractID { get; set; }
        public ProjectsForListDTO Project { get; set; }
        public DateTime SigningDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CompanySigner { get; set; }
        public EmployeeForContractDTO InternalSigner { get; set; }
    }
}
