using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LogRequest
    {
        public int Id { get; set; }
        public string Metodo { get; set; } = string.Empty;
        public string Ruta { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Ip { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public int DuracionMs { get; set; }
        public string? Error { get; set; }
    }
}
