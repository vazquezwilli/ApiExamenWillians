using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class EmpleadoDTO
    {
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; } 
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Email { get; set; }
        public int AreaId { get; set; }
        public DateTime FechaContratacion { get; set; }
    }
}
