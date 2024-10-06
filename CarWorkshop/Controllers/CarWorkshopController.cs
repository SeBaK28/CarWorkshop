using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.CarWorkshop.EditValue;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops;
using CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName;
using CarWorkshop.Application.CarWorkshopService.Commands;
using CarWorkshop.Application.CarWorkshopService.Queries;
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


        public CarWorkshopController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());
            return View(carWorkshops);
        }


        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<ActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            return View(dto);
        }
        [Route("CarWorkshop/{encodedName}/Edit")]

        public async Task<ActionResult> EditInputValue(string encodedName)
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

        public async Task<IActionResult> EditInputValue(string encodedName, GetCarWorkshopByEncodedNameToEdit command) //Edit Workshop
        {                                                                       
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
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command) //Create new workshop
        {                                                                       //Checks whether the workshop exists in the database
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            this.SetNotification("success", $"Created CarWorkshop: {command.Name}");
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [Authorize(Roles = "Owner")]
        [Route("CarWorkshop/CarWorkshopService")]
        public async Task<IActionResult> CreateCarWorkshopService(CreateCarWorkshopServiceCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route("CarWorkshop/{encodedName}/CarWorkshopService")]
        public async Task<IActionResult> GetCarWorkshopServices(string encodedName)
        {
            var data =await  _mediator.Send(new GetCarWorkshopServicesQuery() { EncodedName = encodedName });
            return Ok(data);
        }
    }
}
