using MediatR;


namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommand: CarWorkshopDTO, IRequest //Odpowiada za zapis dancyh, jest to model który odbieramy od usera, dziedziczy po CarWorkshopDTO 
    {                                                               //IRequest to interfejs rozumiany przez MediatR, jest markeram który reprezentuje pytanie z pustą odpowiedzią
        
    }
}
