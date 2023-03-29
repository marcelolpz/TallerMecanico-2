using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using TallerMecanico.Models.Domain;
using TallerMecanico.Models.ViewModels;

namespace TallerMecanico.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private TallerMecanicoDBContext _context;

        public LoginController(ILogger<LoginController> logger, TallerMecanicoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(string Codigo)
        {
            if (!string.IsNullOrEmpty(Codigo) && Codigo == "1")
            {
                ViewBag.Error = "Su sesion ha expirado";
                ViewBag.ClaseMensaje = "alert alert-danger alert-dismissable";
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(UsuarioLoginVm vm)
        {
            var app = vm.ValidarDatosLogin();
            if (!app.IsValid)
            {
                ViewBag.Error = app.Mensaje;
                ViewBag.ClaseMensaje = "alert alert alert-danger alert-dismissable";
                return View(new UsuarioLoginVm());
            }
            var user = _context.Usuario.Where(w => w.Eliminado == false && w.Correo == vm.Email).ProjectToType<UsuarioLoginVm>().FirstOrDefault();
            if (user == null)
            {
                ViewBag.Error = "Usuario o Password son incorrectos.";
                ViewBag.ClaseMensaje = "alert alert-danger alert-dismissable";
                return View(new UsuarioLoginVm());
            }
            if (user.Password != Utilidades.Utilidades.GetMD5(vm.Password))
            {
                ViewBag.Error = "Usuario o Password son incorrectos.";
                ViewBag.ClaseMensaje = "alert alert-danger alert-dismissable";
                return View(new UsuarioLoginVm());
            }

            var modulosRoles = _context.ModulosRoles.Where(w => w.Eliminado == false && w.RolId == user.Rol.Id).ProjectToType<ModulosRolesVm>().ToList();
            var agrupadosId = modulosRoles.Select(s => s.Modulo.AgrupadoModulosId).Distinct().ToList();
            var agrupados = _context.AgrupadoModulos.Where(w => agrupadosId.Contains(w.Id)).ProjectToType<AgrupadoVm>().ToList();
            foreach (var item in agrupados)
            {
                var modulosActuales = modulosRoles.Where(w => w.Modulo.AgrupadoModulosId == item.Id).Select(s => s.Modulo.Id).Distinct().ToList();
                item.Modulos = item.Modulos.Where(w => modulosActuales.Contains(w.Id)).ToList();
            }
            user.Menu = agrupados;
            //user.Password = "";
            var sesionJson = JsonConvert.SerializeObject(user);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(sesionJson);
            var sesionBas64 = System.Convert.ToBase64String(plainTextBytes);
            HttpContext.Session.SetString("usuarioObjeto", sesionBas64);

            return RedirectToAction("Index", "Home");
        }
    }
}
