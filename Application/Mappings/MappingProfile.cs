using AutoMapper;
using Core.DTOS.Application;
using Core.DTOS.Authentication;
using Core.DTOS.User;
using Core.DTOS.Vacancy;
using Core.Entities;
using System.Globalization;
using ApplicationEntity = Core.Entities.Application;

namespace Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            //user mapping
            CreateMap<ApplicationUser , UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); // Ignore roles for now

            CreateMap<UpdateUserDto, ApplicationUser>();

            CreateMap<ApplicationUser, AuthenticationDto>();

            //vacancy
            CreateMap<MangeVacancyDto, Vacancy>()
                .ForMember(dest => dest.ExpiryDate,
               opt => opt.MapFrom(src => DateTimeOffset.ParseExact(src.ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

            CreateMap<Vacancy, VacancyDto>()
                .ForMember(dest => dest.EmployerName , opt => opt.MapFrom(src => $"{src.Employer.FirstName} {src.Employer.LastName}"));

            //Application
            CreateMap<ApplicationEntity, ApplicationDto>()
                .ForMember(dest => dest.EmployerId,
                    opt => opt.MapFrom(src => src.Vacancy.EmployerId)
                )
                .ForMember(dest => dest.VacancyName,
                    opt => opt.MapFrom(src => src.Vacancy.Name)
                )
                .ForMember(dest => dest.ApplicantName,
                    opt => opt.MapFrom(src => $"{src.Applicant.FirstName} {src.Applicant.LastName}")
                )
                .ForMember(dest => dest.ApplicantEmail,
                    opt => opt.MapFrom(src => src.Applicant.Email)
                );
            ;

        }
    }
}
