using AutoMapper;
using DataTransferObjects;
using DataTransferObjects.CityDTOs;
using DataTransferObjects.CompanyContactDTOs;
using DataTransferObjects.CompanyDTOs;
using DataTransferObjects.EmployeeDTOs;
using DataTransferObjects.EmployeePositionDTOs;
using DataTransferObjects.ExternalMentorDTOs;
using DataTransferObjects.ExternalMentorDTOs.ExMentorContact;
using DataTransferObjects.LocationDTOs;
using DataTransferObjects.ProjectContractDTOs;
using DataTransferObjects.ProjectCoveringSubjectDTOs;
using DataTransferObjects.ProjectDTOs;
using DataTransferObjects.ProjectProposalDTOs;
using DataTransferObjects.ScientificAreaDTOs;
using Domain;
using System;

namespace Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityToListDTO>();
            CreateMap<City, CityDetailsDTO>();
            CreateMap<ContactToInsertDTO, Contact>();
            CreateMap<Contact, ContactToViewDTO>();
            CreateMap<Company, CompanyDetailsDTO>();
            CreateMap<Company, CompanyForListDTO>();
            CreateMap<CompanyForInsertDTO, Company>()
                .ForMember(dest => dest.CompanyUsername, 
                opt => opt.MapFrom(p => p.Username))
                .ForMember(dest => dest.Locations,
                opt => opt.MapFrom(p => p.Locations))
                .ForMember(dest => dest.Contacts, 
                opt => opt.MapFrom(p => p.Contacts));
            CreateMap<ExternalMentor, ExternalMentorToViewDTO>();
            CreateMap<ExternalMentor, ExternalMentorForListDTO>();
            CreateMap<LocationInsertCompanyDTO, Location>();
            CreateMap<Location, LocationToViewCompanyDTO>();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<EmployeePosition, EmployeePositionDTO>();
            CreateMap<ExternalMentorContact, ExternalMentorContactDTO>().ReverseMap();
            CreateMap<LocationToUpdateDTO, Location>();
            CreateMap<ScientificArea, ScientificAreaForListDTO>();
            CreateMap<ScientificArea, ScientificAreaDetailsDTO>();
            CreateMap<ExternalMentor, ExternalMentorForProjectProposalDetailDTO>();
            CreateMap<ProjectProposal, ProjectProposalForListDTO>()
                .ForMember(dest => dest.Company,
                opt => opt.MapFrom(p => p.Company));
            CreateMap<ProjectProposal, ProjectProposalDetailsDTO>()
                .ForMember(dest => dest.Company,
                opt => opt.MapFrom(p => p.Company))
                .ForMember(dest => dest.ExternalMentor,
                opt => opt.MapFrom(p => p.ExternalMentor))
                .ForMember(dest => dest.Subjects,
                opt => opt.MapFrom(p => p.Subjects));
            CreateMap<ProjectCoveringSubject, ProjectCoveringSubjectViewDTO>()
                .ForMember(dest => dest.ScientificArea,
                opt => opt.MapFrom(p => p.ScientificArea));
            CreateMap<MentorContactToInsertDTO, ExternalMentorContact>()
                .ForMember(dest => dest.SerialNumber,
                opt => opt.MapFrom(p => p.SerialNumber.GetValueOrDefault()));
            CreateMap<ExternalMentorForInsertProjectDTO, ExternalMentor>()
                .ForMember(dest => dest.MentorID, 
                opt => opt.MapFrom(p => p.MentorID.GetValueOrDefault()));
            CreateMap<ScientificAreaToInsertProjectProposalDTO, ScientificArea>().ReverseMap();
            CreateMap<ProjectCoveringSubjectInsertDTO, ProjectCoveringSubject>()
                .ForMember(dest => dest.ScientificArea,
                opt => opt.MapFrom(p => p.ScientificArea));

            CreateMap<ProjectProposalForInsertDTO, ProjectProposal>()
                .ForMember(dest => dest.ExternalMentor,
                opt => opt.MapFrom(p => p.ExternalMentor))
                .ForMember(dest => dest.Subjects,
                opt => opt.MapFrom(p => p.Subjects))
                .ForMember(dest => dest.Company,
                opt => opt.MapFrom(p => p.Company));


            CreateMap<Company,CompanyForInsertProjectProposalDTO>().ReverseMap();
            CreateMap<Company,CompanyProjectToListDTO>();
            CreateMap<ProjectProposal,ProjectProposalProjectToListDTO>()
                .ForMember(dest => dest.Company,
                opt => opt.MapFrom(p => p.Company));
            CreateMap<Project,ProjectsForListDTO>()
                .ForMember(dest => dest.ProjectProposal,
                opt => opt.MapFrom(p => p.ProjectProposal));
            CreateMap<ProjectCoveringSubject, ProjectCoveringSubjectForProjectDetailsDTO>();
            CreateMap<ProjectProposal, ProjectProposalForProjectDetailsDTO>();
            CreateMap<Project, ProjectDetailsDTO>()
                .ForMember(dest => dest.ProjectProposal,
                opt => opt.MapFrom(p => p.ProjectProposal))
                .ForMember(dest => dest.InternalMentor,
                opt => opt.MapFrom(p => p.InternalMentor))
                .ForMember(dest => dest.DecisionMaker,
                opt => opt.MapFrom(p => p.DecisionMaker));
            CreateMap<ExternalMentor, ExternalMentorForProjectDetailsDTO>();
            CreateMap<ProjectProposalForProjectInsertDTO, ProjectProposal>();
            CreateMap<EmployeeForProjectInsertDTO,Employee>();
            CreateMap<ProjectForInsertDTO, Project>()
                .ForMember(dest => dest.ProjectProposal,
                opt => opt.MapFrom(p => p.ProjectProposal))
                .ForMember(dest => dest.InternalMentor,
                opt => opt.MapFrom(p => p.InternalMentor))
                .ForMember(dest => dest.DecisionMaker,
                opt => opt.MapFrom(p => p.DecisionMaker));
            CreateMap<Employee, EmployeeForContractDTO>();
            CreateMap<ProjectContract, ProjectContractForListDTO>()
                .ForMember(dest => dest.InternalSigner,
                opt => opt.MapFrom(p => p.InternalSigner))
                .ForMember(dest => dest.Project, 
                opt => opt.MapFrom(p => p.Project));
            CreateMap<EmployeeForProjectInsertDTO, Employee>();
            CreateMap<ProjectForInsertContractDTO, Project>();
            CreateMap<ProjectContract, ProjectContractDetailsDTO>()
                .ForMember(dest => dest.InternalSigner,
                opt => opt.MapFrom(p => p.InternalSigner))
                .ForMember(dest => dest.Project,
                opt => opt.MapFrom(p => p.Project));
            CreateMap<ProjectContractForInsertDTO, ProjectContract>()
                .ForMember(dest => dest.InternalSigner,
                opt => opt.MapFrom(p => p.InternalSigner))
                .ForMember(dest => dest.Project,
                opt => opt.MapFrom(p => p.Project));
            CreateMap<EmployeeInternalSignerInsertDTO, Employee>();
            CreateMap<ExternalMentorToInsertDTO, ExternalMentor>();
        }
    }
}
