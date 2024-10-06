using System;
using MediatR;

namespace CarWorkshop.Application.CarWorkshopService.Queries
{
	public class GetCarWorkshopServicesQuery : IRequest<IEnumerable<CarWorkshopServiceDTO>>
	{
		public string EncodedName { get; set; } = default!;
	}
}

