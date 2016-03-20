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
    public class UsuarioDAOSqlServer : IUsuarioDAO
    {
        GestorODBC gestorODBC;
        public UsuarioDAOSqlServer(GestorODBC gestorODBC)
        {
            this.gestorODBC = gestorODBC;
        }

        public Usuario buscarUsuario(string cuentaUsuario, string claveUsuario)
        {
            try
            {
                Usuario usuario = null;
                string consultaSQL = "SELECT codigousuario, nombreusuario, cuentausuario, claveusuario  FROM usuario where cuentausuario=@cuentausuario and claveusuario=@claveusuario ";
                SqlCommand sentencia;
                SqlDataReader resultado;
                sentencia = gestorODBC.prepararSentencia(consultaSQL);
                sentencia.Parameters.Add("@cuentausuario", VarChar, 100).Value = cuentaUsuario;
                sentencia.Parameters.Add("@claveusuario", VarChar, 100).Value = claveUsuario;
                resultado = sentencia.ExecuteReader();
                if (resultado.Read())
                {
                    usuario = new Usuario();
                    usuario.codigoUsuario = (int)resultado[0];
                    usuario.nombreUsuario = (string)resultado[1];
                    usuario.cuentaUsuario = (string)resultado[2];
                    usuario.claveUsuario = (string)resultado[3];
                }
                resultado.Close();
                return usuario;
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorConsultar();
            }
        }

        public void crearUsuario(Usuario usuario)
        {
            try
            {
                string sentenciaSQL = "INSERT INTO usuario(nombreusuario, cuentausuario, claveusuario) VALUES(@nombreusuario, @cuentausuario, @claveusuario)";
                SqlCommand sentencia;
                sentencia = gestorODBC.prepararSentencia(sentenciaSQL);
                sentencia.Parameters.Add("@nombreusuario",VarChar,50).Value=usuario.nombreUsuario;
                sentencia.Parameters.Add("@cuentausuario", VarChar, 100).Value = usuario.cuentaUsuario;
                sentencia.Parameters.Add("@claveusuario", VarChar, 100).Value = usuario.claveUsuario;
                sentencia.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorInsertar();
            }
        }
    }
}
