using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using TallerMecanico.Models.Domain;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using TallerMecanico.Models.ViewModels;

namespace TallerMecanico.Controllers
{
    public class AccessController : Controller
    {
        string urlDomain = "https://localhost:44303/";
        private readonly ILogger<AccessController> _logger;
        private TallerMecanicoDBContext _context;
        public AccessController(ILogger<AccessController> logger, TallerMecanicoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StartRecovery(string token)
        {
            return View();
        }


        [HttpPost]
        public IActionResult StartRecovery(RecoveryVm model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string token = Utilidades.Utilidades.GetMD5((Guid.NewGuid().ToString()));
                var oUser = _context.Usuario.Where(w => w.Eliminado == false && w.Correo == model.Email).FirstOrDefault();

                if (oUser != null)
                {
                    oUser.recovery = token;
                    _context.SaveChanges();
                    ViewBag.Error = "Se ha enviado un correo electronico";
                    ViewBag.ClaseMensaje = "alert alert alert-success alert-dismissable";
                    SendEmail(oUser.Correo, token);
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        [HttpGet]
        public IActionResult Recovery(string token, RecoveryPasswordVm model)
        {
            model.token = token;
            if (model.token == null || model.token.Trim().Equals(""))
            {
                ViewBag.Error = "Tu token a expirado";
                ViewBag.ClaseMensaje = "alert alert alert-danger alert-dismissable";
                return RedirectToAction("Index", "Login");
            }

            var oUser = _context.Usuario.Where(w => w.Eliminado == false && w.recovery == model.token).FirstOrDefault();
            if (oUser == null)
            {
                ViewBag.Error = "Tu token a expirado";
                ViewBag.ClaseMensaje = "alert alert alert-danger alert-dismissable";
                return RedirectToAction("Index", "Login");

            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Recovery(RecoveryPasswordVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    if (model.Password != model.Password2)
                    {
                        ViewBag.Error = "Las contraseñas deben coincidir.";
                        ViewBag.ClaseMensaje = "alert alert alert-danger alert-dismissable";
                    }

                    return View(model);
                }
                var oUser = _context.Usuario.Where(w => w.Eliminado == false && w.recovery == model.token).FirstOrDefault();
                if (oUser != null)
                {
                    string pass = Utilidades.Utilidades.GetMD5(model.Password);
                    oUser.Password = pass;
                    oUser.recovery = null;
                    _context.SaveChanges();
                    ViewBag.Error = "Se ha modificado tu contraseña";
                    ViewBag.ClaseMensaje = "alert alert alert-danger alert-dismissable";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToAction("Index", "Login");
        }

        private void SendEmail(string EmailDestino, string token)
        {
            string EmailOrigen = "tallermecanicoitteam@gmail.com";
            string password = "cqsfutokbovixrsq";
            string url = urlDomain + "Access/Recovery/?token=" + token;

            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperacion de contraseña",
                "<p>Correo para recuperacion de contraseña</p><br>" +
                "<a href='" + url + "'>Click para recuperar</a>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, password);

            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();

        }


    }
}
