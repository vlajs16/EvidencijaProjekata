using DataTransferObjects.EmployeeDTOs;
using DataTransferObjects.ProjectDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataTransferObjects.ProjectContractDTOs
{
    public class ProjectContractForInsertDTO
    {
        [Required]
        public ProjectForInsertContractDTO Project { get; set; }
        [Required]
        public DateTime SigningDate { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string CompanySigner { get; set; }
        [Required]
        public EmployeeInternalSignerInsertDTO InternalSigner { get; set; }
    }
}
