using Examen.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace TallerMecanico.Models.ViewModels
{
    public class UsuarioLoginVm
    {
        public Guid UsuarioId { get; set; }
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Email es campo obligatorio.")]
        [EmailAddress(ErrorMessage = "El Email no es válido.")]

        public string Email { get; set; }
        [Required(ErrorMessage = "El Password es campo obligatorio.")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required(ErrorMessage = "La sucursal es campo obligatorio.")]
        public RolVm Rol { get; set; }
        public List<AgrupadoVm> Menu { get; set; }
        public List<SelectListItem> Sucuesales { get; set; }

        public AppResultVm ValidarDatosLogin()
        {
            if (string.IsNullOrEmpty(this.Email) || string.IsNullOrEmpty(this.Password))
            {
                return AppResultVm.NoSuccess("El Email o Password no pueden estar vacíos.");
            }

            return AppResultVm.Success("Valido", null);
        }
    }
}
