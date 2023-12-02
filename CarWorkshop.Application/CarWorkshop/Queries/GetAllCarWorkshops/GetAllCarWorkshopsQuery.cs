using MediatR;


namespace CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops
{
    public class GetAllCarWorkshopsQuery: IRequest<IEnumerable<CarWorkshopDTO>>//odpowiada za odczyt danych
    {                                                                           //parametr generyczny IRequest jest typu jakiego chcemy odpowiedź z kwerendy
                                                                                //Jako odpowiedź żądamy listy Warsztatów
    }
}
