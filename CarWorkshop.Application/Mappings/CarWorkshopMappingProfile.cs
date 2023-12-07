using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.EditValue;
using CarWorkshop.Application.CarWorkshopService;
using CarWorkshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Mappings
{
    public class CarWorkshopMappingProfile : Profile
    {
        public CarWorkshopMappingProfile(IUserContext userContext)  //po co to mapuję czy to jest do wyświetlania tych informacji dla usera
        {
            var user = userContext.GetCurrentUser();
            CreateMap<CarWorkshopDTO, Domain.Entities.CarWorkshop>()
                .ForMember(e => e.ContactDetails, opt => opt.MapFrom(src => new CarWorkshopContactDetails()
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    PhoneNumber = src.PhoneNumber,
                    Street = src.Street,
                }));

            CreateMap<Domain.Entities.CarWorkshop, CarWorkshopDTO>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => user !=null && (src.CreatedById==user.Id || user.IsInRole("Moderator") ) ))
                .ForMember(dto => dto.Street, opt =>opt.MapFrom(src => src.ContactDetails.Street))
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails.City))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.ContactDetails.PhoneNumber))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode));

            CreateMap<CarWorkshopDTO, GetCarWorkshopByEncodedNameToEdit>();

            CreateMap<CarWorkshopServiceDTO, Domain.Entities.CarWorkshopService>()
                .ReverseMap();
        }
    }
}
