using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.c3_dominio.entidad;
namespace Modelo.c3_dominio.contrato
{
    public interface IProductoImagenDAO
    {
        void crearProductoImagen(ProductoImagen productoimagen, int codigoproducto);
        void modificarProductoImagen(ProductoImagen productoimagen);
        void eliminarProductoImagen(ProductoImagen productoimagen);
        List<ProductoImagen> listarImagenesPorProducto(Producto producto);
        ProductoImagen buscarImagenPrincipalProducto(Producto producto);
    }
}
