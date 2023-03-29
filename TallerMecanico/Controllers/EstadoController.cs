using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using TallerMecanico.Filters;
using TallerMecanico.Models.Domain;
using TallerMecanico.Models.Domain.Entities;
using TallerMecanico.Models.ViewModels;

namespace TallerMecanico.Controllers
{
    public class EstadoController : Controller
    {
        private readonly ILogger<EstadoController> _logger;
        private TallerMecanicoDBContext _context;
        public EstadoController(ILogger<EstadoController> logger, TallerMecanicoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]

        public IActionResult Index()
        {
            var ListaEstados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            return View(ListaEstados);
        }

        [HttpGet]

        public IActionResult Insertar()
        {
            var registro = new EstadoVM();
            var ListaEstados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsTaxis = ListaEstados.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.idEstado.ToString(),
                    Selected = false
                };
            });
            registro.Estados = itemsTaxis;

            return View(registro);
        }

        [HttpPost]

        public IActionResult Insertar(EstadoVM registro)
        {
            var ListaEstados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsTaxis = ListaEstados.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.idEstado.ToString(),
                    Selected = false
                };
            });
            registro.Estados = itemsTaxis;

            var validacion = registro.Validar();

            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(registro);
            }

            var newEntidadRegistro = Estado.Create(
                registro.Nombre


            );

            _context.Estados.Add(newEntidadRegistro);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Editar(Guid Id)
        {

            var registro = _context.Estados.Where(w => w.idEstado == Id && w.Eliminado == false).ProjectToType<EstadoVM>().FirstOrDefault();

            var ListaEstados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsTaxis = ListaEstados.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.idEstado.ToString(),
                    Selected = false
                };
            });
            registro.Estados = itemsTaxis;

            return View(registro);
        }

        [HttpPost]

        public IActionResult Editar(EstadoVM registro)
        {
            var ListaEstados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsTaxis = ListaEstados.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.idEstado.ToString(),
                    Selected = false
                };
            });
            registro.Estados = itemsTaxis;

            var validacion = registro.ValidarEnUpdate();

            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(registro);
            }

            var registroActual = _context.Estados.FirstOrDefault(w => w.idEstado == registro.idEstado);
            registroActual.Update(
                registro.Nombre);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Eliminar(Guid Id)
        {
            var registro = _context.Estados.Where(w => w.idEstado == Id && w.Eliminado == false).ProjectToType<EstadoVM>().FirstOrDefault();

            return View(registro);
        }

        [HttpPost]

        public IActionResult Eliminar(EstadoVM registro)
        {
            var validacion = registro.ValidarEnDelete();
            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(registro);
            }

            var registroActual = _context.Estados.FirstOrDefault(w => w.idEstado == registro.idEstado);
            registroActual.Delete();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

