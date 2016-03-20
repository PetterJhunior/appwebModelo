using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.c3_dominio.contrato;
using Modelo.c3_dominio.entidad;
using System.Data.SqlClient;
using Modelo.c5_transversal.excepcion;
using static System.Data.SqlDbType;
namespace Modelo.c4_persistencia.sqlserver
{
    public class SubCategoriaDAOSqlServer : ISubCategoriaDAO
    {
        GestorODBC gestorODBC;
        public SubCategoriaDAOSqlServer(GestorODBC gestorODBC)
        {
            this.gestorODBC = gestorODBC;
        }

        public SubCategoria buscarProductosPorSubCategoria(int codigoSubCategoria)
        {
            try
            {                
                SubCategoria subCategoria = null;
                string consultaSQL = "SELECT sc.codigosubcategoria,sc.nombresubcategoria FROM subcategoria sc where sc.codigosubcategoria=@codigosubcategoria";
                SqlDataReader resultado;
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(consultaSQL);
                sentencia.Parameters.Add("@codigosubcategoria", Int).Value = codigoSubCategoria;
                resultado = sentencia.ExecuteReader();
                if(resultado.Read())
                {
                    subCategoria = new SubCategoria();
                    subCategoria.codigosubcategoria = (int)resultado[0];
                    subCategoria.nombresubcategoria = (string)resultado[1];
                    subCategoria.listaLineaSubCategoria = listaLineaSubCategoria(subCategoria);
                }
                resultado.Close();
                return subCategoria;
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorConsultar();
            }
        }

        private List<LineaSubCategoria> listaLineaSubCategoria(SubCategoria subCategoria)
        {
            try
            {
                List<LineaSubCategoria> listalineasubcategoria = new List<LineaSubCategoria>();
                LineaSubCategoria lineaSubCategoria;
                string consultaSQL = "SELECT lc.codigolineasubcategoria, lc.nombrelineasubcategoria FROM lineasubcategoria lc where lc.codigosubcategoria=@codigosubcategoria";
                SqlDataReader resultado;
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(consultaSQL);
                sentencia.Parameters.Add("@codigosubcategoria", Int).Value = subCategoria.codigosubcategoria;
                resultado = sentencia.ExecuteReader();
                while (resultado.Read())
                {
                    lineaSubCategoria = new LineaSubCategoria();
                    lineaSubCategoria.codigolinea = (int)resultado[0];
                    lineaSubCategoria.nombrelinea = (string)resultado[1];
                    lineaSubCategoria.listaProductos = listaProductos(lineaSubCategoria);
                    listalineasubcategoria.Add(lineaSubCategoria);
                }
                resultado.Close();
                return listalineasubcategoria;
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorConsultar();
            }
        }
        private List<Producto> listaProductos(LineaSubCategoria lineaSubCategoria)
        {
            try
            {
                List<Producto> listaproductos = new List<Producto>();
                Producto producto;
                string consultaSQL = "SELECT p.codigoproducto, p.nombreproducto, p.descripcionproducto, p.precioproducto, p.detallesproducto FROM producto p where p.codigolineasubcategoria=@codigolineasubcategoria";
                SqlDataReader resultado;
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(consultaSQL);
                sentencia.Parameters.Add("@codigolineasubcategoria", Int).Value = lineaSubCategoria.codigolinea;
                resultado = sentencia.ExecuteReader();
                while (resultado.Read())
                {
                    producto = new Producto();
                    producto.codigoProducto = (int)resultado[0];
                    producto.nombreProducto = (string)resultado[1];
                    producto.descripcionProducto = (string)resultado[2];
                    producto.precioProducto = (decimal)resultado[3];
                    producto.detalleProducto = (string)resultado[4];
                    ProductoImagenDAOSqlServer productoImagenDAO = new ProductoImagenDAOSqlServer(gestorODBC);
                    producto.agregarImagen(productoImagenDAO.buscarImagenPrincipalProducto(producto));
                    listaproductos.Add(producto);
                }
                resultado.Close();
                return listaproductos;
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorConsultar();
            }
        }
        
        public SubCategoria buscarSubCategoria(int codigoSubCategoria)
        {
            try
            {
                SubCategoria subCategoria = null;
                string consultaSQL = "SELECT sc.codigosubcategoria,sc.nombresubcategoria FROM subcategoria sc where sc.codigosubcategoria=@codigosubcategoria";
                SqlDataReader resultado;
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(consultaSQL);
                sentencia.Parameters.Add("@codigosubcategoria", Int).Value = codigoSubCategoria;
                resultado = sentencia.ExecuteReader();
                if (resultado.Read())
                {
                    subCategoria = new SubCategoria();
                    subCategoria.codigosubcategoria = (int)resultado[0];
                    subCategoria.nombresubcategoria = (string)resultado[1];
                }
                resultado.Close();
                return subCategoria;
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorConsultar();
            }
        }

        public List<SubCategoria> buscarSubCategorias(Categoria categoria)
        {
            try
            {
                List<SubCategoria> listaSubCategorias = new List<SubCategoria>();
                SubCategoria subCategoria = null;
                string consultaSQL = "SELECT sc.codigosubcategoria,sc.nombresubcategoria FROM subcategoria sc where sc.codigocategoria=@codigocategoria";
                SqlDataReader resultado;
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(consultaSQL);
                sentencia.Parameters.Add("@codigocategoria", Int).Value = categoria.codigocategoria;
                resultado = sentencia.ExecuteReader();
                while (resultado.Read())
                {
                    subCategoria = new SubCategoria();
                    subCategoria.codigosubcategoria = (int)resultado[0];
                    subCategoria.nombresubcategoria = (string)resultado[1];
                    listaSubCategorias.Add(subCategoria);
                }
                resultado.Close();
                return listaSubCategorias;
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorConsultar();
            }
        }

        public void crearSubCategoria(SubCategoria subCategoria, int codigoCategoria)
        {
            try
            {
                string sentenciaSQL = "INSERT INTO subcategoria(codigocategoria, nombresubcategoria)VALUES (@codigocategoria, @nombresubcategoria);";
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(sentenciaSQL);
                sentencia.Parameters.Add("codigocategoria", Int).Value = codigoCategoria;
                sentencia.Parameters.Add("@nombresubcategoria", VarChar, 50).Value = subCategoria.nombresubcategoria;
                sentencia.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorInsertar();
            }
        }

        public void editarSubCategoria(SubCategoria subCategoria)
        {
            try
            {
                string sentenciaSQL = "update subcategoria set nombresubcategoria=@nombresubcategoria where codigosubcategoria = @codigosubcategoria ";
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(sentenciaSQL);
                sentencia.Parameters.Add("@nombresubcategoria", VarChar, 50).Value = subCategoria.nombresubcategoria;
                sentencia.Parameters.Add("@codigosubcategoria", Int).Value = subCategoria.codigosubcategoria;
                sentencia.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorModificar();
            }
        }

        public void eliminarSubCategoria(SubCategoria subCategoria)
        {
            try
            {
                string sentenciaSQL = "DELETE FROM subcategoria WHERE codigosubcategoria = @codigosubcategoria";
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(sentenciaSQL);
                sentencia.Parameters.Add("@codigosubcategoria", Int).Value = subCategoria.codigosubcategoria;
                sentencia.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorEliminar();
            }
        }
    }
}
