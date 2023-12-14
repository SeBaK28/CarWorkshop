using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopService.Commands
{
    public class CreateCarWorkshopServiceCommand : CarWorkshopServiceDTO, IRequest
    {
        public string CarWorkshopEncodedName { get; set; } = default!;
    }
}
