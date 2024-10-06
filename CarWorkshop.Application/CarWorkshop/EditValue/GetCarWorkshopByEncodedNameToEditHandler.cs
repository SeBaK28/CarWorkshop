using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.EditValue
{
    public class GetCarWorkshopByEncodedNameToEditHandler : IRequestHandler<GetCarWorkshopByEncodedNameToEdit>
    {
        private readonly ICarWorkshopRepository _repository;
        private readonly IUserContext _userContext;

        public GetCarWorkshopByEncodedNameToEditHandler(ICarWorkshopRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(GetCarWorkshopByEncodedNameToEdit request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _repository.GetByEncodedName(request.EncodedName!);
            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && (carWorkshop.CreatedById == user.Id || user.IsInRole("Moderator"));
            if(isEditable)
            {
                return Unit.Value;
            }

            carWorkshop.Name = request.Name;
            carWorkshop.Description = request.Description;

            carWorkshop.About = request.About;
            carWorkshop.ContactDetails.City = request.City;
            carWorkshop.ContactDetails.PhoneNumber = request.PhoneNumber;
            carWorkshop.ContactDetails.Street = request.Street;
            carWorkshop.ContactDetails.PostalCode = request.PostalCode;

            await _repository.Commit();

            return Unit.Value;
        }
    }
}
