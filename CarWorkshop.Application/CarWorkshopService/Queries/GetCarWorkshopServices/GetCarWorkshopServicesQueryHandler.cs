using System;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace CarWorkshop.Application.CarWorkshopService.Queries.GetCarWorkshopServices
{
    public class GetCarWorkshopServicesQueryHandler : IRequestHandler<GetCarWorkshopServicesQuery, IEnumerable<CarWorkshopServiceDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;
        public GetCarWorkshopServicesQueryHandler(ICarWorkshopServiceRepository carWorkshopServiceRepository, IMapper mapper)
        {
            _mapper = mapper;
            _carWorkshopServiceRepository = carWorkshopServiceRepository;
        }
        public async Task<IEnumerable<CarWorkshopServiceDTO>> Handle(GetCarWorkshopServicesQuery request, CancellationToken cancellationToken)
        {
            var result = await _carWorkshopServiceRepository.GetAllByEncodedName(request.EncodedName);
            var dtos = _mapper.Map<IEnumerable<CarWorkshopServiceDTO>>(result);

            return dtos;
        }
    }
}

