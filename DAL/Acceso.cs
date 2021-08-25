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


        public string Escribir(string Consulta, Hashtable Parametros)
        {
            ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = AbrirConexion();

            string Id = ""; // Valor que se capturará en el caso de insersiones

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
                ComandoSQL.Parameters.Add("@Id_ins", SqlDbType.Int).Direction = ParameterDirection.Output; // Para inserción

                int respuesta = ComandoSQL.ExecuteNonQuery();
                Transaccion.Commit();
                Id = ComandoSQL.Parameters["@Id_ins"].Value.ToString().Trim();
            }

            catch (Exception ex)
            {
                string Error = ex.Message;
                Transaccion.Rollback();
            }

            finally
            {
                Conexion.Close();
            }
            return Id;
        }

    }
}
