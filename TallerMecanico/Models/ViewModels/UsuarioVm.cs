using Examen.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace TallerMecanico.Models.ViewModels
{
    public class UsuarioVm
    {
        public Guid UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public bool Eliminado { get; set; }
        public Guid RolId { get; set; }
        public RolVm Rol { get; set; }
        public List<SelectListItem> Roles { set; get; }
        public AppResultVm Validar()
        {
            AppResultVm app = new AppResultVm();

            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El nombre no puede esta vacío \n";

            }
            if (string.IsNullOrEmpty(this.Apellido))
            {
                app.Mensaje += "El apellido no puede esta vacío \n";

            }
            if (string.IsNullOrEmpty(this.Correo))
            {
                app.Mensaje += "El correo no puede esta vacío \n";

            }
            if (string.IsNullOrEmpty(this.Password))
            {
                app.Mensaje += "La contraseña no puede esta vacía \n";

            }
            if (this.RolId == Guid.Empty)
            {
                app.Mensaje += "Debe seleccionar un rol \n";

            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Registro finalizado con éxito.";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }

        public AppResultVm ValidarEnUpdate()
        {
            AppResultVm app = new AppResultVm();

            if (this.UsuarioId == Guid.Empty)
            {
                app.Mensaje += "El ID no debe estar vacio \n";
            }
            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El nombre no puede esta vacío \n";

            }
            if (string.IsNullOrEmpty(this.Apellido))
            {
                app.Mensaje += "El apellido no puede esta vacío \n";

            }
            if (string.IsNullOrEmpty(this.Correo))
            {
                app.Mensaje += "El correo no puede esta vacío \n";

            }
            if (this.RolId == Guid.Empty)
            {
                app.Mensaje += "Debe seleccionar un rol \n";

            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Registro finalizado con éxito.";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }

        public AppResultVm ValidarEnDelete()
        {
            AppResultVm app = new AppResultVm();

            if (this.UsuarioId == Guid.Empty)
            {
                app.Mensaje += "El ID no debe estar vacio \n";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Registro eliminado con éxito.";
            }
            else
            {
                app.IsValid = false;
            }

            return app;
        }
    }
}
