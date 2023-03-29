using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using TallerMecanico.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace TallerMecanico.Filters
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string modulo) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new ModuloVm { Nombre = modulo } };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private UsuarioLoginVm UsuarioObjeto;
       
        readonly ModuloVm _claim;

        public ClaimRequirementFilter(ModuloVm claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            try
            {
                string sesionBase64 = filterContext.HttpContext.Session.GetString("usuarioObjeto");
                
                if (string.IsNullOrEmpty(sesionBase64))
                {
                    filterContext.Result = new RedirectResult("~/Login/Index?Codigo=1");
                    return;
                }
                var base64EncodedBytes = System.Convert.FromBase64String(sesionBase64);
                var sesion = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                UsuarioObjeto = JsonConvert.DeserializeObject<UsuarioLoginVm>(sesion);
              

                if (UsuarioObjeto == null)
                {
                    filterContext.Result = new RedirectResult("~/Login/Index?Codigo=1");
                    return;
                }
                var encontro = false;
                foreach (var item in UsuarioObjeto.Menu)
                {
                    var modusloact = item.Modulos.FirstOrDefault(w => w.Nombre.Trim().ToLower() == _claim.Nombre.ToLower());
                    encontro = modusloact != null;
                    if (encontro)
                    {
                        break;
                    }
                }
                if (!encontro && _claim.Nombre.ToLower() != "principal")
                {
                    filterContext.Result = new RedirectResult("~/Home/Index?Codigo=1");
                    return;
                }
            }



            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Home/Index?Codigo=1");
            }
        }

    }
}
