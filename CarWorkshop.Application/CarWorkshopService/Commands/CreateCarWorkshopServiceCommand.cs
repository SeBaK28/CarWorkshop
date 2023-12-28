using MediatR;


namespace CarWorkshop.Application.CarWorkshopService.Commands
{
    public class CreateCarWorkshopServiceCommand : CarWorkshopServiceDTO, IRequest
    {
        public string CarWorkshopEncodedName { get; set; } = default!;
    }
}
