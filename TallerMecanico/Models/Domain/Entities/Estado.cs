using System;

namespace TallerMecanico.Models.Domain.Entities
{
    public class Estado
    {
        public Guid idEstado { get; set; }
        public string Nombre { get; set; }
        public bool Eliminado { get; set; }

        public static Estado Create(string nombre)
        {
            return new Estado
            {
                idEstado = Guid.NewGuid(),
                Nombre = nombre,
                Eliminado = false

            };
        }

        public void Update(string nombre)
        {
            this.Nombre = nombre;
            this.Eliminado = false;
        }

        public void Delete()
        {
            this.Eliminado = true;
        }
    }
    
}
