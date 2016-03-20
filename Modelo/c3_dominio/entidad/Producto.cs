using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.c5_transversal.excepcion;
namespace Modelo.c3_dominio.entidad
{
    public class Producto
    {
        public Producto()
        {
            this.codigoProducto = 0;
            this.listaImagenes = new List<ProductoImagen>();
        }
        public int codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string descripcionProducto { get; set; }
        public decimal precioProducto { get; set; }
        public string detalleProducto { get; set; }
        public List<ProductoImagen> listaImagenes { get; set; }
        public void validarPrecio()
        {
            if (this.precioProducto <= 0)
                throw ExcepcionReglaNegocio.crearErrorPrecioProducto();
        }
        public void agregarImagen(ProductoImagen productoimagen)
        {
            verificarExistenciaImagen(productoimagen);
            listaImagenes.Add(productoimagen);
        }
        public void verificarExistenciaImagen(ProductoImagen productoimagen)
        {
            foreach (ProductoImagen productoImagenVerificar in listaImagenes)
            {
                if (productoImagenVerificar.urlimagen.Equals(productoimagen.urlimagen))
                    throw ExcepcionReglaNegocio.crearErrorExistenciaImagen();
            }
        }

        public void quitarImagen(string urlimagen)
        {
            foreach (ProductoImagen productoImagenVerificar in listaImagenes)
            {
                if (productoImagenVerificar.urlimagen.Equals(urlimagen))
                {
                    listaImagenes.Remove(productoImagenVerificar);
                    break;
                }    
            }
        }

   }
}
