using System.Collections.Generic;
using System;

namespace TallerMecanico.Models.ViewModels
{
    public class AgrupadoVm
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public List<ModuloVm> Modulos { get; set; }
    }
}
