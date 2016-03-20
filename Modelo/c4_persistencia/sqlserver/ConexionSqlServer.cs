using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.c4_persistencia;
using Modelo.c5_transversal.excepcion;
using System.Data.SqlClient;

namespace Modelo.c4_persistencia.sqlserver
{
    public class ConexionSqlServer : GestorODBC
    {
        public override void abrirConexion()
        {
            try
            {
                string url = "data source=DESKTOP-BR8KL83;initial catalog=paginaweb2;persist security info=True;user id=desarrollador;password=123;MultipleActiveResultSets=True;";
                conexion = new SqlConnection(url);
                conexion.Open();
            }
            catch (Exception)
            {
                throw ExcepcionSQL.crearErrorAbrirConexion(); ;
            }
        }
    }
}
