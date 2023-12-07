using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.CarWorkshop.EditValue;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops;
using CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName;
using CarWorkshop.Extensions;
using CarWorkshop.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System.Security.Principal;

namespace CarWorkshop.Controllers
{
    public class CarWorkshopController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

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

        /*public ActionResult Create() //zwraca formularz w którym user będzie w stanie wypisać konkretne informacje, na ich podstawie trafią one później do "Create(CreateCarWorkshopCommand comman)"
        {
            return View();
        }*/
        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<ActionResult> Details(string encodedName) 
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            return View(dto);
        }
        [Route("CarWorkshop/{encodedName}/Edit")]
        //[Authorize (Roles ="Moderator")]
        public async Task<ActionResult> EditInputValue(string encodedName)  //akcja mająca za zadanie edycję InputValue
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }
            GetCarWorkshopByEncodedNameToEdit model = _mapper.Map<GetCarWorkshopByEncodedNameToEdit>(dto);

            return View(model);
        }
        [HttpPost]
        [Route("CarWorkshop/{encodedName}/Edit")]
        //[Authorize]
        public async Task<IActionResult> EditInputValue(string encodedName, GetCarWorkshopByEncodedNameToEdit command) //edycja warsztatu
        {                                                                       //sprawdzanie czy tworzony warsztat już isniteje w bazie danych
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Owner")]
        public IActionResult Create()
        {
            /* if (User.Identity == null || User.Identity.IsAuthenticated) //ten if jest zastąpiony --> [Authorize]
             {
                 return RedirectToPage("/Account/Login", new { Area = "Identity" });
             }*/
            //if (!User.IsInRole("Owner")) //ten if jest zastąpiony [Authorize(Roles = "Owner")]
            //{
            //    return RedirectToAction("NoAccess", "Home");
            //}
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command) //tworzenie nowego warsztatu
        {                                                                       //sprawdzanie czy tworzony warsztat już isniteje w bazie danych
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            this.SetNotification("success", $"Created CarWorkshop: {command.Name}");
            return RedirectToAction(nameof(Index));
        }
    }
}
