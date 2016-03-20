using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.c3_dominio.contrato;
using Modelo.c3_dominio.entidad;
using System.Data.SqlClient;
using static System.Data.SqlDbType;
using Modelo.c5_transversal.excepcion;

namespace Modelo.c4_persistencia.sqlserver
{
    public class ProductoImagenDAOSqlServer : IProductoImagenDAO
    {
        GestorODBC gestorODBC;
        public ProductoImagenDAOSqlServer(GestorODBC gestorODBC)
        {
            this.gestorODBC = gestorODBC;
        }

        public ProductoImagen buscarImagenPrincipalProducto(Producto producto)
        {
            try
            {
                ProductoImagen productoImagen = null;
                string consultaSQL = "SELECT img.codigoimagenproducto,  img.direccionimagenproducto, img.nombreimagenproducto, img.principalimagenproducto  FROM imagenproducto img where img.codigoproducto=@codigoproducto and img.principalimagenproducto='TRUE'";
                SqlDataReader resultado;
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(consultaSQL);
                sentencia.Parameters.Add("@codigoproducto", Int).Value = producto.codigoProducto;
                resultado = sentencia.ExecuteReader();
                if (resultado.Read())
                {
                    productoImagen = new ProductoImagen();
                    productoImagen.codigoimagen = (int)resultado[0];
                    productoImagen.urlimagen = (string)resultado[1];
                    productoImagen.nombreimagen = (string)resultado[2];
                    productoImagen.principal = (bool)resultado[3];
                }
                resultado.Close();
                return productoImagen;
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorConsultar();
            }
        }

        public void crearProductoImagen(ProductoImagen productoimagen, int codigoproducto)
        {
            try
            {
                string sentenciaSQL = "INSERT INTO imagenproducto(codigoproducto,direccionimagenproducto,nombreimagenproducto,principalimagenproducto) VALUES(@codigoproducto,@direccionimagenproducto,@nombreimagenproducto,@principalimagenproducto)";
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(sentenciaSQL);
                sentencia.Parameters.Add("@codigoproducto", Int).Value = codigoproducto;
                sentencia.Parameters.Add("@direccionimagenproducto", VarChar, 500).Value = productoimagen.urlimagen;
                sentencia.Parameters.Add("@nombreimagenproducto", VarChar, 100).Value = productoimagen.nombreimagen;
                sentencia.Parameters.Add("@principalimagenproducto", Bit).Value = productoimagen.principal;
                sentencia.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorInsertar();
            }
        }

        public void eliminarProductoImagen(ProductoImagen productoimagen)
        {
            try
            {
                string sentenciaSQL = "delete imagenproducto where codigoimagenproducto=@codigoimagenproducto";
                SqlCommand sentencia;
                sentencia= gestorODBC.prepararSentencia(sentenciaSQL);
                sentencia.Parameters.Add("@codigoimagenproducto", Int).Value = productoimagen.codigoimagen;
                sentencia.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorEliminar();
            }
        }

        public List<ProductoImagen> listarImagenesPorProducto(Producto producto)
        {
            try
            {
                List<ProductoImagen> listaproductoimagen = new List<ProductoImagen>();
                ProductoImagen productoImagen = null;
                string consultaSQL = "select img.codigoimagenproducto,img.direccionimagenproducto, img.nombreimagenproducto, img.principalimagenproducto from imagenproducto img where img.codigoproducto = @codigoProducto ";
                SqlCommand sentencia;
                SqlDataReader resultado;
                sentencia = gestorODBC.prepararSentencia(consultaSQL);
                sentencia.Parameters.Add("@codigoProducto", Int).Value = producto.codigoProducto;
                resultado = sentencia.ExecuteReader();
                while (resultado.Read())
                {
                    productoImagen = new ProductoImagen();
                    productoImagen.codigoimagen = (int)resultado[0];
                    productoImagen.urlimagen = (string)resultado[1];
                    productoImagen.nombreimagen = (string)resultado[2];
                    productoImagen.principal = (bool)resultado[3];
                    producto.agregarImagen(productoImagen);
                    listaproductoimagen.Add(productoImagen);
                }
                resultado.Close();
                return listaproductoimagen;
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorConsultar(); ;
            }
        }

        public void modificarProductoImagen(ProductoImagen productoimagen)
        {
            try
            {
                string sentenciaSQL = "UPDATE imagenproducto SET direccionimagenproducto=@direccionimagenproducto,  nombreimagenproducto=@nombreimagenproducto, principalimagenproducto=@principalimagenproducto WHERE codigoimagenproducto=@codigoimagenproducto";
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(sentenciaSQL);
                sentencia.Parameters.Add("@direccionimagenproducto", VarChar, 100).Value = productoimagen.urlimagen;
                sentencia.Parameters.Add("@nombreimagenproducto", VarChar, 200).Value = productoimagen.nombreimagen;
                sentencia.Parameters.Add("@principalimagenproducto", Bit).Value = productoimagen.principal;
                sentencia.Parameters.Add("@codigoimagenproducto", Int).Value = productoimagen.codigoimagen;
                sentencia.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorModificar();
            }
        }
    }
}
