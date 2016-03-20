using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.c3_dominio.entidad
{
    public class Contacto
    {
        public Contacto()
        {
            this.codigoContacto = 0;
            this.visto = false;
        }
        public int codigoContacto { get; set; }
        public string nombreContacto { get; set; }
        public string correoContacto { get; set; }
        public string comentarioContacto { get; set; }
        public bool visto { get; set; }
    }
}
