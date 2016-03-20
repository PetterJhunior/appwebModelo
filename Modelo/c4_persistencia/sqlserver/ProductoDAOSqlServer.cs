﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.c3_dominio.contrato;
using Modelo.c3_dominio.entidad;
using Modelo.c5_transversal.excepcion;
using System.Data.SqlClient;
using static System.Data.SqlDbType;
using Modelo.c4_persistencia.sqlserver;
namespace Modelo.c4_persistencia
{
    
    public class ProductoDAOSqlServer : IProductoDAO
    {
        GestorODBC gestorODBC;

        public ProductoDAOSqlServer(GestorODBC gestorODBC)
        {
            this.gestorODBC = gestorODBC;
        }
        public List<Producto> buscarProductos()
        {
            try
            {
                List<Producto> listaproductos = new List<Producto>();
                string consultaSQL = "select  p.codigoproducto, p.nombreproducto, p.descripcionproducto,p.detallesproducto, p.precioproducto from producto p ";
                SqlDataReader resultado;
                resultado = gestorODBC.ejecutarConsulta(consultaSQL);
                while (resultado.Read())
                {
                    Producto producto = new Producto();
                    producto.codigoProducto = (int)resultado[0];
                    producto.nombreProducto = (string)resultado[1];
                    producto.descripcionProducto = (string)resultado[2];
                    producto.detalleProducto = (string)resultado[3];
                    producto.precioProducto = (decimal)resultado[4];
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

        public Producto buscarProducto(int codigoProducto)
        {
            try
            {
                Producto producto = null;
                string consultaSQL = "select  p.codigoproducto, p.nombreproducto, p.descripcionproducto,p.detallesproducto, p.precioproducto  from producto p where p.codigoproducto= @codigoProducto ";
                SqlCommand sentencia;
                SqlDataReader resultado;
                sentencia = gestorODBC.prepararSentencia(consultaSQL);
                sentencia.Parameters.Add("@codigoProducto", Int).Value = codigoProducto;
                resultado = sentencia.ExecuteReader();
                if (resultado.Read())
                {
                    producto = new Producto();
                    producto.codigoProducto = (int)resultado[0];
                    producto.nombreProducto = (string)resultado[1];
                    producto.descripcionProducto = (string)resultado[2];
                    producto.detalleProducto = (string)resultado[3];
                    producto.precioProducto = (decimal)resultado[4];
                    ProductoImagenDAOSqlServer productoImagenDAO = new ProductoImagenDAOSqlServer(gestorODBC);
                    producto.listaImagenes = productoImagenDAO.listarImagenesPorProducto(producto);
                }
                resultado.Close();
                return producto;
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorConsultar();
            }
        }

        public void crearProducto(Producto producto, int codigoLinea)
        {
            try
            {
                string sentenciaSQL = "insert into producto (codigolineasubcategoria,nombreproducto,descripcionproducto,precioproducto,detallesproducto) values (@codigolineasubcategoria,@nombreProducto,@descripcionproducto,@precioproducto,@detallesproducto)";
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(sentenciaSQL);
                sentencia.Parameters.Add("@codigolineasubcategoria", Int).Value = codigoLinea;
                sentencia.Parameters.Add("@nombreproducto",VarChar, 100).Value = producto.nombreProducto;
                sentencia.Parameters.Add("@descripcionproducto",VarChar, 200).Value = producto.descripcionProducto;
                sentencia.Parameters.Add("@precioproducto", System.Data.SqlDbType.Decimal).Value = producto.precioProducto;
                sentencia.Parameters.Add("@detallesproducto", VarChar, 900).Value = producto.detalleProducto;
                sentencia.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorInsertar();
            }
        }

        public void eliminarProducto(Producto producto)
        {
            try
            {
                string sentenciaSQL = "DELETE FROM producto WHERE codigoproducto = @codigoproducto";
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(sentenciaSQL);
                sentencia.Parameters.Add("@codigoproducto", Int).Value = producto.codigoProducto;
                sentencia.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorEliminar();
            }
        }

        public void modificarProducto(Producto producto)
        {
            try
            {
                string sentenciaSQL = "UPDATE producto   SET nombreproducto=@nombreproducto,  descripcionproducto=@descripcionproducto, precioproducto=@precioproducto, detallesproducto=@descripcionproducto WHERE codigoproducto=@codigoproducto";
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(sentenciaSQL);
                sentencia.Parameters.Add("@nombreproducto", VarChar, 100).Value = producto.nombreProducto;
                sentencia.Parameters.Add("@descripcionproducto", VarChar, 200).Value = producto.descripcionProducto;
                sentencia.Parameters.Add("@precioproducto", System.Data.SqlDbType.Decimal).Value = producto.precioProducto;
                sentencia.Parameters.Add("@detallesproducto", VarChar, 900).Value = producto.detalleProducto;
                sentencia.Parameters.Add("@codigoproducto", Int).Value = producto.codigoProducto;
                sentencia.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorModificar();
            }
        }        
    }
}
