using Examen.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace TallerMecanico.Models.ViewModels
{
    public class EstadoVM
    {
        public Guid idEstado { get; set; }
        public string Nombre { get; set; }
        public bool Eliminado { get; set; }
        public List<SelectListItem> Estados { set; get; }
        public AppResultVm Validar()
        {
            AppResultVm app = new AppResultVm();

            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El nombre no puede esta vacío \n";

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

            if (this.idEstado == Guid.Empty)
            {
                app.Mensaje += "El ID no debe estar vacio \n";
            }
            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El nombre no puede esta vacío \n";

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

            if (this.idEstado == Guid.Empty)
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

