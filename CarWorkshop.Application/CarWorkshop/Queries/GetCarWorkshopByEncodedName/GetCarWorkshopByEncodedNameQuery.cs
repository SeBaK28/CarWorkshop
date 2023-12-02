using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName
{
    public  class GetCarWorkshopByEncodedNameQuery : IRequest<CarWorkshopDTO>//odpowiada za odczyt danych, służy do wyświetlanie szczegółowych danych danego Warsztatu
    {
        public string EncodedName {  get; set; }
        public GetCarWorkshopByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
