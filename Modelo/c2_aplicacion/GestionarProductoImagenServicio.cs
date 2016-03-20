using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.c3_dominio.entidad;
using Modelo.c3_dominio.contrato;
using Modelo.c4_persistencia;
using Modelo.c4_persistencia.sqlserver;

namespace Modelo.c2_aplicacion
{
    public class GestionarProductoImagenServicio
    {
        GestorODBC gestorODBC;
        IProductoImagenDAO productoImagenDAO;
        public GestionarProductoImagenServicio()
        {
            gestorODBC = new ConexionSqlServer();
            productoImagenDAO = new ProductoImagenDAOSqlServer(gestorODBC);
        }

        public ProductoImagen buscarImagenPrincipalProducto(Producto producto)
        {
            try
            {
                gestorODBC.abrirConexion();
                ProductoImagen productoImagen;
                productoImagen = productoImagenDAO.buscarImagenPrincipalProducto(producto);
                gestorODBC.cerrarConexion();
                return productoImagen;
            }
            catch (Exception e)
            {
                gestorODBC.cerrarConexion();
                throw e;
            }
        }

        public void crearProductoImagen(ProductoImagen productoimagen, int codigoproducto)
        {
            try
            {
                gestorODBC.abrirConexion();
                productoImagenDAO.crearProductoImagen(productoimagen, codigoproducto);
                gestorODBC.cerrarConexion();
            }
            catch (Exception e)
            {
                gestorODBC.cerrarConexion();
                throw e;
            }
        }

        public void eliminarProductoImagen(ProductoImagen productoimagen)
        {
            try
            {
                gestorODBC.abrirConexion();
                productoimagen.validarPrincipal(productoimagen);
                productoImagenDAO.eliminarProductoImagen(productoimagen);
                gestorODBC.cerrarConexion();
            }
            catch (Exception e)
            {
                gestorODBC.cerrarConexion();
                throw e;
            }
        }

        public List<ProductoImagen> listarImagenesPorProducto(Producto producto)
        {
            try
            {
                gestorODBC.abrirConexion();
                List<ProductoImagen> listaProductoImagenPorProducto;
                listaProductoImagenPorProducto = productoImagenDAO.listarImagenesPorProducto(producto);
                gestorODBC.cerrarConexion();
                return listaProductoImagenPorProducto;
            }
            catch (Exception e)
            {
                gestorODBC.cerrarConexion();
                throw e;
            }
        }

        public void modificarProductoImagen(ProductoImagen productoimagen)
        {
            try
            {
                gestorODBC.abrirConexion();
                productoImagenDAO.modificarProductoImagen(productoimagen);
                gestorODBC.cerrarConexion();
            }
            catch (Exception e)
            {
                gestorODBC.cerrarConexion();
                throw e;
            }
        }
    }
}
