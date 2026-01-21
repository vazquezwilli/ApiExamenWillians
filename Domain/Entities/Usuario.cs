using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Username { get; set; } 
        public string PasswordHash { get; set; }
        public int RolId { get; set; }
        public bool Activo { get; set; }

        public Rol Rol { get; set; }
    }
}
