using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.c3_dominio.entidad
{
    public class Usuario
    {
        public Usuario()
        {
            this.codigoUsuario = 0;
        }
        public int codigoUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string cuentaUsuario { get; set; }
        public string claveUsuario { get; set; }

    }
}
