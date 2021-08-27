using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DAL
{
    public class Acceso
    {
        private SqlConnection Conexion;
        private SqlTransaction Transaccion;
        private SqlCommand ComandoSQL;
        private string CadenaConexion = "Data Source=.;Initial Catalog=a2cloud;Integrated Security=True";

        private SqlConnection AbrirConexion()

        {
            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            return Conexion;
        }

        public DataSet LeerDatos(string Consuta, Hashtable Parametros)

        {
            DataSet DS = new DataSet();
            ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = AbrirConexion();
            ComandoSQL.CommandText = Consuta;
            ComandoSQL.CommandType = CommandType.StoredProcedure;

            if ((Parametros != null))
            {

                foreach (string dato in Parametros.Keys)
                {
                    ComandoSQL.Parameters.AddWithValue(dato, Parametros[dato]);
                }
            }

            SqlDataAdapter Adaptador = new SqlDataAdapter(ComandoSQL);
            Adaptador.Fill(DS);
            return DS;
        }


        public int Escribir(string Consulta, Hashtable Parametros)
        {
            ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = AbrirConexion();

            int Afectadas;

            try
            {
                Transaccion = Conexion.BeginTransaction();
                ComandoSQL.CommandText = Consulta;
                ComandoSQL.CommandType = CommandType.StoredProcedure;
                ComandoSQL.Transaction = Transaccion;

                if ((Parametros != null))
                {
                    foreach (string dato in Parametros.Keys)
                    {
                        ComandoSQL.Parameters.AddWithValue(dato, Parametros[dato]);
                    }
                }
               
                Afectadas = ComandoSQL.ExecuteNonQuery();
                Transaccion.Commit();          
            }

            catch (Exception ex)
            {               
                Transaccion.Rollback();
                return -1;
            }

            finally
            {
                Conexion.Close();
            }
            return Afectadas;
        }

    }
}
