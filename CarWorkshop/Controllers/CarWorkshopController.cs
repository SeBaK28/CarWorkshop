using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.CarWorkshop.EditValue;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops;
using CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Principal;

namespace CarWorkshop.Controllers
{
    public class CarWorkshopController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;

        //private readonly ICarWorkshopService _carWorkshopService;

        public CarWorkshopController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index() //Wyświetla wszystkie utworzone warsztaty samochodowe
        {
            var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());//.Send przyjmuje obiekt zapytania
            return View(carWorkshops);
        }

        public ActionResult Create() //zwraca formularz w którym user będzie w stanie wypisać konkretne informacje, na ich podstawie trafią one później do "Create(CreateCarWorkshopCommand comman)"
        {
            return View();
        }
        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<ActionResult> Details(string encodedName) 
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            return View(dto);
        }
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<ActionResult> EditInputValue(string encodedName)  //akcja mająca za zadanie edycję InputValue
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            GetCarWorkshopByEncodedNameToEdit model = _mapper.Map<GetCarWorkshopByEncodedNameToEdit>(dto);

            return View(model);
        }
        [HttpPost]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> EditInputValue(string encodedName, GetCarWorkshopByEncodedNameToEdit command) //edycja warsztatu
        {                                                                       //sprawdzanie czy tworzony warsztat już isniteje w bazie danych
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command) //tworzenie nowego warsztatu
        {                                                                       //sprawdzanie czy tworzony warsztat już isniteje w bazie danych
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}
