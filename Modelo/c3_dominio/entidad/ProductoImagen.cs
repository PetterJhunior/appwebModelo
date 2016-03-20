using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.c5_transversal.excepcion;
namespace Modelo.c3_dominio.entidad
{
    public class ProductoImagen
    {
        public ProductoImagen()
        {
            this.codigoimagen = 0;
        }
        public int codigoimagen { get; set; }
        public string nombreimagen { get; set; }
        public string urlimagen { get; set; }
        public bool principal { get; set; }
        public void validarPrincipal(ProductoImagen productoImagen)
        {
            if (productoImagen.principal == true)
                throw ExcepcionReglaNegocio.crearErrorImagenPrincipal();
        }
    }
}
