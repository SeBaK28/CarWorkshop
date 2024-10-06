using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;


namespace CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops
{
    public class GetAllCarWorkshopsQueryHandler : IRequestHandler<GetAllCarWorkshopsQuery, IEnumerable<CarWorkshopDTO>> 
    {
        private readonly ICarWorkshopRepository _carWorkshopRepository;
        private readonly IMapper _mapper;

        public GetAllCarWorkshopsQueryHandler(ICarWorkshopRepository carWorkshopRepository, IMapper mapper) 
        {
            _carWorkshopRepository = carWorkshopRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarWorkshopDTO>> Handle(GetAllCarWorkshopsQuery request, CancellationToken cancellationToken)
        {
            var carWorkshops = await _carWorkshopRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<CarWorkshopDTO>>(carWorkshops);

            return dtos;
        }
    }
}
